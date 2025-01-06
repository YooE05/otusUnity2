using UniRx;

namespace Homeworks.UpgradeManager
{
    public sealed class UpgradePresenter
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        public ReactiveCommand OnMoneyChanged { get; private set; } = new ReactiveCommand();

        private readonly Upgrade _upgrade;
        private readonly MoneyStorage _moneyStorage;

        public string UpgradeName => _upgrade.Id;

        public string Level => _upgrade.Level.ToString();

        public string MaxLevel => _upgrade.MaxLevel.ToString();

        public string Price => _upgrade.NextPrice.ToString();

        public string Value => _upgrade.GetCurrentValue();

        public UpgradePresenter(Upgrade upgrade, MoneyStorage moneyStorage)
        {
            _upgrade = upgrade;
            _moneyStorage = moneyStorage;

            _moneyStorage.Money.Subscribe(delegate { OnMoneyChanged.Execute(); })
                .AddTo(_disposables);
        }

        public ButtonState GetButtonState()
        {
            if (_upgrade.IsMaxLevel) return ButtonState.MaxValueReached;

            return _moneyStorage.IsEnoughMoney(_upgrade.NextPrice) ? ButtonState.CanBuy : ButtonState.CantBuy;
        }

        public void DoUpgrade()
        {
            _upgrade.LevelUp();
            _moneyStorage.SpendMoney(_upgrade.NextPrice);
        }

        ~UpgradePresenter()
        {
            _disposables.Dispose();
        }
    }
}