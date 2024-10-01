using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
    public abstract class Pool<T> : MonoBehaviour, Listeners.IInitListener where T : Object
    {
        [SerializeField] protected Transform _parent;
        [SerializeField] protected Transform _releasedParent;
        [SerializeField] private T _prefab;
        [SerializeField] protected int _initCount;

        private readonly Queue<T> _poolQueue = new Queue<T>();
        private readonly List<T> _activeObjects = new List<T>();

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
            T poolObject = Instantiate(_prefab, _parent);
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