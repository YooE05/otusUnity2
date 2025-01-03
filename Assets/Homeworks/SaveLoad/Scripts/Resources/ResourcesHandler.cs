using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public sealed class ResourcesHandler : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private int _resourceValue;

        private PlayerResources _playerResources;

        [Inject]
        private void Construct(PlayerResources playerResources)
        {
            _playerResources = playerResources;
            _resources = playerResources.Resources;
            _playerResources.OnNewValueSetted += ShowNewValue;
        }

        [ShowInInspector]
        public void SetUpNewValue()
        {
            _playerResources.SetResource(_resourceType, _resourceValue);
           }

        private void ShowNewValue(ResourceType type, int value)
        {
            _resources[type] = value;
            Debug.Log($"Resourse {type} view updated to value {value}");
        }

        private void OnDestroy()
        {
            if (_playerResources != null) _playerResources.OnNewValueSetted -= ShowNewValue;
        }
    }
}