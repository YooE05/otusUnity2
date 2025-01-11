using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Homeworks.SaveLoad
{
    public sealed class PlayerResourcesStorage
    {
        public Action<ResourceType, int> OnNewValueSetted;

        [ShowInInspector, ReadOnly]
        private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

        public Dictionary<ResourceType, int> Resources => _resources;

        public void SetResource(ResourceType resourceType, int resource)
        {
            _resources[resourceType] = resource;
            OnNewValueSetted?.Invoke(resourceType, resource);
        }
    }
}