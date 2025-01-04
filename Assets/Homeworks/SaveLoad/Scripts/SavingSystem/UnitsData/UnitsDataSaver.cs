namespace Homeworks.SaveLoad
{
    public sealed class UnitsDataSaver: DataSaver<UnitData[], UnitsHandler>
    {
        protected override UnitData[] ExtractData(UnitsHandler service)
        {
            return service.GetAllUnitsData();
        }

        protected override void SetupData(UnitsHandler service, UnitData[] data)
        {
            service.SetupUnits(data);
        }
    }
}