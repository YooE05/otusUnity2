using Zenject;

namespace Homeworks.UpgradeManager
{
    public sealed class OutAreaCapacityUpgrade : Upgrade
    {
        private StationHandler _stationHandler;
        private readonly OutAreaConfigUpgrade _config;

        public OutAreaCapacityUpgrade(OutAreaConfigUpgrade config) : base(config)
        {
            _config = config;
        }

        [Inject]
        public void Construct(StationHandler stationHandler)
        {
            _stationHandler = stationHandler;
            SetCapacity();
        }

        public override string GetCurrentValue()
        {
            return _stationHandler.GetOutCapacity().ToString();
        }

        protected override void OnUpgrade()
        {
            SetCapacity();
        }

        private void SetCapacity()
        {
            _stationHandler.SetOutCapacity(_config.GetCapacity(Level));
        }
    }
}