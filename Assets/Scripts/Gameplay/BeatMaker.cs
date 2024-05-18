using System;
using System.Threading;
using ServiceLocator;
using UnityEngine;

namespace Gameplay
{
    //создаёт бит
    public class BeatMaker : IService, IDisposable
    {
        private readonly int _bpm;
        private readonly AudioSource _beat;
        
        private CancellationTokenSource _cancellationToken = new();
        
        public float Time { get; private set; }
        
        public event Action Hited;

        public BeatMaker(int bpm, AudioSource beat)
        {
            _bpm = bpm;
            _beat = beat;
            Make();
        }
        
        //при вызове будет создавать бит с указаной частотой и посылать сигнал в момент удара
        public async void Make()
        {
            _cancellationToken = new();
            float delay = 60f / _bpm;
            
            while (!_cancellationToken.IsCancellationRequested)
            {
                await Awaitable.NextFrameAsync(_cancellationToken.Token);
                
                Time -= UnityEngine.Time.deltaTime;
                if (Time > 0) continue;
                
                Time = delay;
                _beat.Play();
                Hited?.Invoke();
            }
        }

        public void Stop() => _cancellationToken.Cancel();

        public void Dispose()
        {
            Stop();
            _cancellationToken.Dispose();
        }
    }
}