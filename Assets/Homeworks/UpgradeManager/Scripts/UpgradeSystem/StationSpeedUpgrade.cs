using Zenject;

namespace Homeworks.UpgradeManager
{
    public sealed class StationSpeedUpgrade : Upgrade
    {
        private StationHandler _stationHandler;
        private readonly StationSpeedConfigUpgrade _speedConfig;

        public StationSpeedUpgrade(StationSpeedConfigUpgrade config) : base(config)
        {
            _speedConfig = config;
        }

        [Inject]
        public void Construct(StationHandler stationHandler)
        {
            _stationHandler = stationHandler;
            SetNewStationTime();
        }

        public override string GetCurrentValue()
        {
            var value = _stationHandler.TimeToTransform;
            return value.ToString();
        }

        protected override void OnUpgrade()
        {
            SetNewStationTime();
        }

        private void SetNewStationTime()
        {
            var newTime = _speedConfig.GetNewTime(Level);
            _stationHandler.SetTransformationSpeed(newTime);
        }
    }
}