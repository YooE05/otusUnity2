using Homeworks.UpgradeManager;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private StationHandler _stationHandler;
    [SerializeField] private int _initMoneyAmount = 1000;

    public override void InstallBindings()
    {
        Container.Bind<StationHandler>().FromInstance(_stationHandler).AsSingle().NonLazy();
        Container.Bind<MoneyStorage>().AsSingle().WithArguments(_initMoneyAmount).NonLazy();
    }
}