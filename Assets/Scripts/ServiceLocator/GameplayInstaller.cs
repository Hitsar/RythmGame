using Gameplay;
using Gameplay.String;
using TMPro;
using UnityEngine;

namespace ServiceLocator
{
    //инициализирует сервис локатор и регистрирует сервисы
    public class GameplayInstaller : Installer
    {
        [SerializeField] private MelodyCreator _melodyCreator;
        [SerializeField] private NotesView _notesView;
        [Header("Beat Maker")]
        [SerializeField, Min(1)] private int _bpm;
        [SerializeField] private AudioSource _beat;
        [Header("String Notes Spawner")]
        [SerializeField] private SpriteRenderer[] _stringNotes;
        [SerializeField] private Transform[] _stings;
        [SerializeField] private Transform[] _spawnPoints;
        [Header("Score")]
        [SerializeField] private TMP_Text _playerScoreText;
        [SerializeField] private TMP_Text _enemyScoreText;
        [Header("Achievements Handler")]
        [SerializeField] private TMP_Text _achievementText;
        [Header("Judge")]
        [SerializeField] private TMP_Text _endTextPanel;
        private BeatMaker _beatMaker;
        private StringNotesSpawner _stringNotesSpawner;
        private Score _score;
        private AchievementHandler _achievementHandler;
        private Judge _judge;
        
        private void Awake()
        {
            ServiceLocator.Initialize();
            
            ServiceLocator.Current.Register(_beatMaker = new(_bpm, _beat));
            ServiceLocator.Current.Register(_notesView);
            ServiceLocator.Current.Register(_melodyCreator);
            ServiceLocator.Current.Register(_score = new(_playerScoreText, _enemyScoreText));
            //классы которые не нуждаются в MonoBehaviour создаются тут
            _stringNotesSpawner = new(_stringNotes, _stings, _spawnPoints);
            _achievementHandler = new(_achievementText);
            _judge = new(_endTextPanel);
        }

        private void OnDestroy()
        {
            _stringNotesSpawner.Dispose();
            _score.Dispose();
            _beatMaker.Dispose();
            _achievementHandler.Dispose();
            _judge.Dispose();
        }
    }
}