using System;
using System.Collections.Generic;
using Gameplay.String;
using ServiceLocator;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    //генерирует ноты для мелодии
    public class MelodyCreator : MonoBehaviour, IService
    {
        [SerializeField] private NoteData[] _notes;
        [SerializeField] private int _hitsAwait;
        [SerializeField] private int _roundCount;
        private int _currentRound;
        
        private int _hitsPassed;
        private int _currentHitsCount;
        
        private NotesView _notesView;
        private BeatMaker _beatMaker;

        private readonly NoteData[] _currentMelody = new NoteData[8];
        public IReadOnlyList<NoteData> CurrentMelody => _currentMelody;
        public bool IsCreated { get; private set; }

        public event Action NoteAdded;
        public event Action Finished;

        public void Init()
        {
            _notesView = ServiceLocator.ServiceLocator.Current.Get<NotesView>();
            _beatMaker = ServiceLocator.ServiceLocator.Current.Get<BeatMaker>();
            
            _beatMaker.Hited += Create;
            _currentHitsCount = Random.Range(4, 9);
        }

        private void Create()
        {
            //если надо, пропускаем. например если ход игрока
            _hitsAwait--;
            if (_hitsAwait > 0) return;
            if (_roundCount == _currentRound)
            {
                _beatMaker.Stop();
                Finished?.Invoke();
                return;
            }
            
            IsCreated = false;
            //радномно выбираем ноту, добавляем в список, отображаем и воспроизводим звук
            // если = 1 то нота пропускается
            if (Random.Range(0, 4) == 0) _currentMelody[_hitsPassed] = null;
            else
            {
                NoteData note = _notes[Random.Range(0, _notes.Length)];
                _currentMelody[_hitsPassed] = note;
                _notesView.Display(note);
                note.Audio.Play();
                NoteAdded?.Invoke();
            }

            _hitsPassed++;
            //если все ноты созданы ты говорим об этом и готовимся к следующему разу
            if (_hitsPassed != _currentHitsCount) return;
            
            IsCreated = true;
            _hitsAwait = _currentHitsCount + 1;
            _hitsPassed = 0;
            _currentRound++;
            _currentHitsCount = Random.Range(4, 9);
        }

        private void OnDestroy() => _beatMaker.Hited -= Create;

        [Serializable]
        public class NoteData
        {
            [field: SerializeField] public StringSfx Audio { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
            [field: SerializeField] public int Id { get; private set; }
        }
    }
}