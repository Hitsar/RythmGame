using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.MainMenu
{
    //класс отвечает за появления текста при нажатии на бармена
    public class BarmenPhraser : MonoBehaviour
    {
        [SerializeField] private string[] _phrases;
        [SerializeField] private Transform _textPosition;
        [SerializeField] private float _hidingDelay;
        private TMP_Text _text;
        private Image _image;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            _image = GetComponentInParent<Image>();
        }

        //вызывается по нажатии на кнопку (в инспекторе настроено)
        public void Say()
        {
            //останавливаем ранее запущеные анимации
            _text.DOKill();
            _image.DOKill();
            _image.transform.DOKill();
            //меняем текст
            _text.text = _phrases[Random.Range(0, _phrases.Length)];
            //запускаем анимации появления, и скрытия текста с задержкой
            _text.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            _image.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            _image.transform.DOLocalMoveY(_textPosition.localPosition.y, 0.6f).SetEase(Ease.OutCubic);
            
            _text.DOFade(0, 0.5f).SetEase(Ease.OutCubic).SetDelay(_hidingDelay);
            _image.DOFade(0, 0.5f).SetEase(Ease.OutCubic).SetDelay(_hidingDelay);
            _image.transform.DOLocalMoveY(0, 0.6f).SetEase(Ease.OutCubic).SetDelay(_hidingDelay);
        }
    }
}