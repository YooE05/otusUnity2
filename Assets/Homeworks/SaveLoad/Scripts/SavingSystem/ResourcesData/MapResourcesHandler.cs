using System.Collections.Generic;
using System.Linq;

namespace Homeworks.SaveLoad
{
    public sealed class MapResourcesHandler
    {
        private readonly List<ResourceObject> _objects;
        public MapResourcesHandler(EntitiesContainer resourcesContainer)
        {
            _objects = resourcesContainer.ResourceObjects;
        }

        public void SetupObjects(int[] remainingData)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].RemainingCount = remainingData[i];
                _objects[i].gameObject.SetActive(_objects[i].RemainingCount > 0);
            }
        }

        public int[] GetObjectsData()
        {
            var remainValues = _objects.Select(obj => obj.RemainingCount).ToArray();
            return remainValues;
        }
    }
}