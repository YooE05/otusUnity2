using UnityEngine;
using Zenject;

namespace Homeworks.UpgradeManager
{
    public class StationSpeedUpgrade : Upgrade
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
        }

        public override string GetCurrentValue()
        {
            var value = _stationHandler.TimeToTransform;
            Debug.Log("In Upgrade " + value);
            return value.ToString();
        }

        protected override void OnUpgrade()
        {
            var newTime = _speedConfig.GetNewTime(Level);
            _stationHandler.SetTransformationSpeed(newTime);
        }
    }
}