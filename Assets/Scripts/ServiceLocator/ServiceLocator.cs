using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator
{
    //ServiceLocator это паттерн. поэтому лучше о нём почтитать в интернете.
    //если кратко, это синглтон который хранит в себе сервисы(классы) и у него их можно запрашивать, регистрировать и удалять.
    //удобно если проект мелкий и не требует di контейнера. логика крайне проста поэтому для этого проекта самое то.
    public class ServiceLocator
    {
        private readonly Dictionary<Type, IService> _services = new();

        public static ServiceLocator Current { get; private set; }

        public static void Initialize() => Current = new();

        public T Get<T>() where T : IService
        {
            Type key = typeof(T);
            if (!_services.ContainsKey(key)) throw new InvalidOperationException($"{key.Name} not registered with {GetType().Name}.");
            
            return (T)_services[key];
        }
      
        public void Register<T>(T service) where T : IService
        {
            Type key = typeof(T);
            if (_services.ContainsKey(key)) throw new InvalidOperationException($"Attempted to register service of type {key.Name} which is already registered with the {GetType().Name}.");

            service.Init();
            _services.Add(key, service);
        }

        public void Unregister<T>() where T : IService
        {
            Type key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                Debug.LogWarning($"Attempted to unregister service of type {key.Name} which is not registered with the {GetType().Name}.");
                return;
            }

            _services.Remove(key);
        }

        public void UnregisterAll() => _services.Clear();

        public void InitializeAllSystems() { foreach (IService service in _services.Values) service.Init(); }
    }
}