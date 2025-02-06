using System.Collections.Generic;
using UnityEngine;

namespace Homeworks.UpgradeManager
{
    public sealed class ObjectSpawnArea : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objects;

        private int _capacity = 2;
        private int _takenCount;
        private int AvailableCount => _capacity - _takenCount;
        public bool HasEmptySlots => AvailableCount > 0;
        public bool HasTakenSlots => _takenCount > 0;

        public int Capacity => _capacity;

        private void Awake()
        {
            _takenCount = 0;
            SetObjectsView();
        }

        public bool TryTake(int needSlotsCount, out int restSlotsCount)
        {
            if (!HasEmptySlots)
            {
                restSlotsCount = needSlotsCount;
                return false;
            }

            var addedSlots = Mathf.Clamp(needSlotsCount, 0, AvailableCount);
            _takenCount += addedSlots;

            SetObjectsView();

            restSlotsCount = needSlotsCount - addedSlots;
            return true;
        }

        public void Release()
        {
            _takenCount = Mathf.Clamp(_takenCount - 1, 0, _takenCount);
            SetObjectsView();
        }

        public void ReleaseAll()
        {
            _takenCount = 0;
            SetObjectsView();
        }

        public void SetCapacityCount(int newCapacity)
        {
            _capacity = newCapacity;
        }

        private void SetObjectsView()
        {
            var enabledObjectsCount = Mathf.Clamp(_takenCount, 0, _objects.Count);
            for (var i = 0; i < _objects.Count; i++)
            {
                _objects[i].SetActive(i < enabledObjectsCount);
            }
        }
    }
}