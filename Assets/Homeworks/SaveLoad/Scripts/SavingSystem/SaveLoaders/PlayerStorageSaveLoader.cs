using System.Collections.Generic;

namespace Homeworks.SaveLoad
{
    public class PlayerStorageSaveLoader : DataSaveLoader<Dictionary<ResourceType, int>, PlayerResourcesStorage>
    {
        protected override Dictionary<ResourceType, int> ExtractData(PlayerResourcesStorage service)
        {
            return service.Resources;
        }

        protected override void SetupData(PlayerResourcesStorage service, Dictionary<ResourceType, int> data)
        {
            foreach (var resourceData in data)
            {
                service.SetResource(resourceData.Key, resourceData.Value);
            }
        }

        protected override void SetupDefaultData(PlayerResourcesStorage service)
        {
            service.SetResource(ResourceType.FOOD, 10);
            service.SetResource(ResourceType.WOOD, 5);
            service.SetResource(ResourceType.STONE, 3);
            service.SetResource(ResourceType.MONEY, 100);
        }
    }
}