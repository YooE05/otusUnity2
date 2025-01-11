using System.Collections.Generic;
using System.Linq;
using GameEngine;

namespace Homeworks.SaveLoad
{
    public sealed class ResoucesSaveLoader : DataSaveLoader<List<ResourceData>, ResourceService>
    {
        protected override List<ResourceData> ExtractData(ResourceService service)
        {
            var resources = service.GetResources().ToList();
            List<ResourceData> resourcesData = new List<ResourceData>();

            for (int i = 0; i < resources.Count; i++)
            {
                ResourceData data = new ResourceData
                {
                    Id = resources[i].ID,
                    Amount = resources[i].Amount
                };

                resourcesData.Add(data);
            }

            return resourcesData;
        }

        protected override void SetupData(ResourceService service, List<ResourceData> data)
        {
            var sceneResources = service.GetResources();
            ResourceData resourceData;

            foreach (var resource in sceneResources)
            {
                resourceData = data.Find(resData => resData.Id == resource.ID);
                resource.Amount = resourceData.Amount;
            }
        }
    }

    public sealed class ResourceData
    {
        public string Id;
        public int Amount;
    }
}