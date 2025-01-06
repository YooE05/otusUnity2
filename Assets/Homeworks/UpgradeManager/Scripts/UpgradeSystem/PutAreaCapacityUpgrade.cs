using Zenject;

namespace Homeworks.UpgradeManager
{
    public sealed class PutAreaCapacityUpgrade : Upgrade
    {
        private StationHandler _stationHandler;
        private readonly PutAreaConfigUpgrade _config;

        public PutAreaCapacityUpgrade(PutAreaConfigUpgrade config) : base(config)
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
            return _stationHandler.GetPutCapacity().ToString();
        }

        protected override void OnUpgrade()
        {
            SetCapacity();
        }

        private void SetCapacity()
        {
            var newCapacity = _config.GetCapacity(Level);
            _stationHandler.SetPutCapacity(newCapacity);
        }
    }
}