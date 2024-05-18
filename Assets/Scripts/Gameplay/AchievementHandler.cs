using System;
using DG.Tweening;
using Gameplay.String;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    //определяет когда будет получено достижение
    public class AchievementHandler : IDisposable
    {
        private readonly TMP_Text _text;
        private int _successfulClickCount;
        private MelodyCreator _melodyCreator;
        private bool _isGoalAchieved;

        public AchievementHandler(TMP_Text text)
        {
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.ACHIEVEMENT) == 1) return;
            _text = text;
            Init();
        }

        private void Init()
        {
            _melodyCreator = ServiceLocator.ServiceLocator.Current.Get<MelodyCreator>();
            _melodyCreator.Finished += TryGetAchievement;
            
            StringEvents.NiceHit += AddClick;
            StringEvents.PerfectHit += AddClick;
            StringEvents.Away += RemoveProgress;
        }

        private void TryGetAchievement()
        {
            //если цель не достигнута - отменяем
            if (!_isGoalAchieved) return;
            //сохраняем то что мы получили достижение, и отображаем на экране
            PlayerPrefs.SetInt(PlayerPrefsKeys.ACHIEVEMENT, 1);
            _text.gameObject.SetActive(true);
            _text.DOFade(1, 0.5f);
            _text.DOFade(0, 0.5f).SetDelay(2.5f).OnKill(() => _text.gameObject.SetActive(false));
        }

        //кажное успешное нажатие продвигает прогресс
        private void AddClick()
        {
            _successfulClickCount++;
            if (_successfulClickCount >= 10) GoalAchieved();
        }

        //неверное нажание сбрасывает прогресс
        private void RemoveProgress() => _successfulClickCount = 0;

        private void GoalAchieved()
        {
            _isGoalAchieved = true;
            StringEvents.NiceHit -= AddClick;
            StringEvents.PerfectHit -= AddClick;
            StringEvents.Away -= RemoveProgress;
        }
        
        public void Dispose()
        {
            _melodyCreator.Finished -= TryGetAchievement;
            GoalAchieved();
        }
    }
}