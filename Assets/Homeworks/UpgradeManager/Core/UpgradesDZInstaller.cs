using Homeworks.UpgradeManager;
using UnityEngine;
using Zenject;

public class UpgradesDZInstaller : MonoInstaller
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private StationHandler _stationHandler;
    [SerializeField] private UpgradePanelView _upgradePanel;
    [SerializeField] private int _initMoneyAmount = 1000;

    public override void InstallBindings()
    {
        Container.Bind<UpgradeManager>().FromInstance(_upgradeManager).AsSingle().NonLazy();
        Container.Bind<UpgradePanelView>().FromInstance(_upgradePanel).AsSingle().NonLazy();
        Container.Bind<StationHandler>().FromInstance(_stationHandler).AsSingle().NonLazy();

        Container.Bind<MoneyStorage>().AsSingle().WithArguments(_initMoneyAmount).NonLazy();
    }
}