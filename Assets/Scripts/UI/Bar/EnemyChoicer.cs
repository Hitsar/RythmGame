using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    //класс отвечает за список врагов (уровней)
    public class EnemyChoicer : MonoBehaviour
    {
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private Image _fadeImage, _enemyImage;
        [SerializeField] private Button _choiceButton;
        private int _currentIndex;

        private void Start() => Next(0);

        //вызывается при нажатии на кнопки влело вправо
        public void Next(int index)
        {
            //прибавляем к индексу текущего уровня 1 или -1 (зависит от стрелки) и проверяем на валидность (не выходит за массив)
            _currentIndex += index;
            if (_currentIndex >= _enemies.Length) _currentIndex = 0;
            if (_currentIndex < 0) _currentIndex = _enemies.Length - 1;
            //меняем спрайт врага и подписываем метод который переносит нас на уровень выбраного врага
            _enemyImage.sprite = _enemies[_currentIndex].Sprite;
            _choiceButton.onClick.RemoveAllListeners();
            _choiceButton.onClick.AddListener(() => Choice(_enemies[_currentIndex]));
            //выключаем кнопку на не рабочих уронях
            _choiceButton.interactable = _currentIndex == 0;
        }
        //затемняет экран и включает сцену
        private void Choice(Enemy enemy) => _fadeImage.DOFade(1, 1).OnKill(() => SceneManager.LoadScene(enemy.Scene));
    }

    [Serializable]
    public struct Enemy
    {
        [field: SerializeField] public string Scene { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}