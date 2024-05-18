using System;
using Gameplay.String;
using ServiceLocator;
using TMPro;

namespace Gameplay
{
    //занимается подсчётом очков и их отображением
    public class Score : IService, IDisposable
    {
        private readonly TMP_Text _playerText, _enemyText;
        private int _player;
        private int _enemy;

        private MelodyCreator _melodyCreator;

        public Score(TMP_Text playerText, TMP_Text enemyText)
        {
            _playerText = playerText;
            _enemyText = enemyText;
        }
        
        public void Init()
        {
            _melodyCreator = ServiceLocator.ServiceLocator.Current.Get<MelodyCreator>();
            
            _melodyCreator.NoteAdded += AddEnemyScore;
            StringEvents.PerfectHit += AddPlayerPerfectScore;
            StringEvents.NiceHit += AddPlayerNiceScore;
        }

        public bool IsPlayerWin() => _player >= _enemy;

        //добавление очков игроку
        private void AddPlayerScore(int score)
        {
            _player += score;
            _playerText.text = _player.ToString();
        }

        //вспомогательные методы для подписки на ивент
        private void AddPlayerPerfectScore() => AddPlayerScore(20);
        private void AddPlayerNiceScore() => AddPlayerScore(10);
        
        //добавление очков врагу
        private void AddEnemyScore()
        {
            _enemy += 10;
            _enemyText.text = _enemy.ToString();
        }

        public void Dispose()
        {
            _melodyCreator.NoteAdded -= AddEnemyScore;
            StringEvents.PerfectHit -= AddPlayerPerfectScore;
            StringEvents.NiceHit -= AddPlayerNiceScore;
        }
    }
}