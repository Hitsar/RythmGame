using System;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Judge : IDisposable
    {
        private readonly TMP_Text _text;
        private MelodyCreator _melodyCreator;
        private Score _score;

        public Judge(TMP_Text text)
        {
            _text = text;
            Init();
        }
        
        private void Init()
        {
            _melodyCreator = ServiceLocator.ServiceLocator.Current.Get<MelodyCreator>();
            _score = ServiceLocator.ServiceLocator.Current.Get<Score>();

            _melodyCreator.Finished += Summarize;
        }

        private void Summarize()
        {
            if (_score.IsPlayerWin()) _text.text = "Победа!";
            _text.transform.parent.gameObject.SetActive(true);
        }

        public void Dispose() => _melodyCreator.Finished -= Summarize;
    }
}