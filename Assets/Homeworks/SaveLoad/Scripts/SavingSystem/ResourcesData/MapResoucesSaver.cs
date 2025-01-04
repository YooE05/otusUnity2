namespace Homeworks.SaveLoad
{
    public class MapResoucesSaver : DataSaver<int[], MapResourcesHandler>
    {
        protected override int[] ExtractData(MapResourcesHandler service)
        {
            var data = service.GetObjectsData();
            return data;
        }

        protected override void SetupData(MapResourcesHandler service, int[] data)
        {
            service.SetupObjects(data);
        }
    }
}