using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public sealed class PlayerStorageHandler : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private int _resourceValue;

        private PlayerResourcesStorage _playerResourcesStorage;

        [Inject]
        private void Construct(PlayerResourcesStorage playerResourcesStorage)
        {
            _playerResourcesStorage = playerResourcesStorage;
            _resources = playerResourcesStorage.Resources;
        }

        [ShowInInspector]
        public void SetUpNewValue()
        {
            _playerResourcesStorage.SetResource(_resourceType, _resourceValue);
        }
    }
}