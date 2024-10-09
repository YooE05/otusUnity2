using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
    public abstract class Pool<T> : Listeners.IInitListener where T : Object
    {
        protected Transform _parent;
        protected Transform _releasedParent;
        protected T _prefab;
        protected int _initCount;

        private readonly Queue<T> _poolQueue = new Queue<T>();
        private readonly List<T> _activeObjects = new List<T>();

        private GamecycleManager _gamecycleManager;

        [Inject]
        public void Construct(GamecycleManager gamecycleManager)
        {
            _gamecycleManager = gamecycleManager;
        }

        public void OnInit()
        {
            InitObjects();
        }

        private void InitObjects()
        {
            for (var i = 0; i < _initCount; i++)
            {
                AddObject();
            }
        }

        private void AddObject()
        {
            T poolObject = Object.Instantiate(_prefab, _parent);
            _gamecycleManager.AddListeners(poolObject);
            Return(poolObject);
        }

        protected void Return(T objectToReturn)
        {
            _poolQueue.Enqueue(objectToReturn);
            _activeObjects.Remove(objectToReturn);
        }

        protected T Get()
        {
            if (_poolQueue.TryDequeue(out var item))
            {
                _activeObjects.Add(item);
                return item;
            }
            else
            {
                AddObject();
                return Get();
            }
        }

        protected List<T> GetActive()
        {
            return _activeObjects;
        }
    }
}