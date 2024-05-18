using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    //по нажатию на кнопку играть, кнопки главного меню уходят за экран
    public class MainMenuButtonsAnimator : MonoBehaviour
    {
        [SerializeField] private Button _playButton, _settingsButton, _achievementButton;
        [SerializeField] private Image _fadePanel;

        private float _startPositionPlay;
        private float _startPositionSettings;

        private void Awake()
        {
            _startPositionPlay = _playButton.transform.localPosition.y;
            _startPositionSettings = _settingsButton.transform.localPosition.x;
        }

        private void OnEnable()
        {
            _playButton.transform.DOLocalMoveY(_startPositionPlay, 1).SetEase(Ease.OutCubic);
            _settingsButton.transform.DOLocalMoveX(_startPositionSettings, 1).SetEase(Ease.OutCubic);
            _achievementButton.transform.DOLocalMoveX(-_startPositionSettings, 1).SetEase(Ease.OutCubic);
            
            _fadePanel.DOFade(0.86f, 0.9f).SetEase(Ease.OutCubic);
        }

        public void Hide()
        {
            _playButton.transform.DOLocalMoveY(750, 1).SetEase(Ease.InCubic).OnKill(() => gameObject.SetActive(false));
            _settingsButton.transform.DOLocalMoveX(-1250, 1).SetEase(Ease.InCubic);
            _achievementButton.transform.DOLocalMoveX(1250, 1).SetEase(Ease.InCubic);
            
            _fadePanel.DOFade(0, 0.9f).SetEase(Ease.InCubic);
        }
    }
}