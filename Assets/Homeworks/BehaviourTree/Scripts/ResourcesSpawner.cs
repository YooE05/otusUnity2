using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Homeworks.BehaviourTree
{
    public class ResourcesSpawner : MonoBehaviour
    {
        [SerializeField] private List<Resource> _resources = new();

        public bool IsResourcesAvailable => _resources.Find(r => r.IsReadyToGet);

        [SerializeField] private float _cooldownMin;
        [SerializeField] private float _cooldownMax;
        private float _timer;

        private void Awake()
        {
            for (int i = 0; i < _resources.Count; i++)
            {
                _resources[i].Get();
            }
        }

        public void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                SpawnTree();
                _timer = Random.Range(_cooldownMin, _cooldownMax);
            }
        }

        public List<Resource> GetAvailableResources()
        {
            var res = _resources.FindAll(r => r.IsReadyToGet);
            return res;
        }

        private void SpawnTree()
        {
            var readyRes = _resources.FindAll(r => !r.IsReadyToGet);
            if (readyRes.Count > 0)
            {
                var randRes = readyRes[Random.Range(0, readyRes.Count - 1)];
                randRes.Enable();
            }
        }
    }
}