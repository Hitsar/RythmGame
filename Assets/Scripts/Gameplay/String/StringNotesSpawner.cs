using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.String
{
    //спавнит бегущие на струны точки
    public class StringNotesSpawner : IDisposable
    {
        private readonly SpriteRenderer[] _stringNotes;
        private readonly Transform[] _stings;
        private readonly Transform[] _spawnPoints;

        private MelodyCreator _melodyCreator;
        private BeatMaker _beatMaker;
        private int _hitsPassed;
        private int _missedHits;

        public StringNotesSpawner(SpriteRenderer[] stringNotes, Transform[] stings, Transform[] spawnPoints)
        {
            _stringNotes = stringNotes;
            _stings = stings;
            _spawnPoints = spawnPoints;
            Init();
        }
        
        private void Init()
        {
            _melodyCreator = ServiceLocator.ServiceLocator.Current.Get<MelodyCreator>();
            _beatMaker = ServiceLocator.ServiceLocator.Current.Get<BeatMaker>();
            //если режим сложный точки не отображаются
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.DIFFICULTY, 0) == 1)
                foreach (SpriteRenderer spriteRenderer in _stringNotes) spriteRenderer.color = Color.clear;
            
            _beatMaker.Hited += TrySpawnNote;
        }

        private void TrySpawnNote()
        {
            if (!_melodyCreator.IsCreated)
            {
                _hitsPassed = 0;
                return;
            }
            if (_melodyCreator.CurrentMelody.Count == _hitsPassed) return;
            if (_melodyCreator.CurrentMelody[_hitsPassed] != null)
            {
                //меняем спрайт ноты, ставим над струной, включаем и двигаем в сторону струны
                SpriteRenderer note = _stringNotes[_hitsPassed];
                var noteData = _melodyCreator.CurrentMelody[_hitsPassed];

                note.sprite = noteData.Sprite;
                note.transform.position = _spawnPoints[noteData.Id].position;
                note.gameObject.SetActive(true);
                note.transform.DOMove(_stings[noteData.Id].position * 1.05f, _beatMaker.Time * 1.1f).SetEase(Ease.Linear).OnKill(() => note.gameObject.SetActive(false));
            }

            _hitsPassed++;
        }

        public void Dispose() => _beatMaker.Hited -= TrySpawnNote;
    }
}