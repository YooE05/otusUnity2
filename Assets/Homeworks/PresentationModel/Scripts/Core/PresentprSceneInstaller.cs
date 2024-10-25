using UnityEngine;
using Zenject;

public class PresentprSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        
        /*Container.BindInterfacesAndSelfTo<GamecycleManager>().AsSingle().NonLazy();
        Container.Bind<LevelBounds>().FromInstance(_levelBounds).AsSingle();
        Container.BindInterfacesAndSelfTo<BulletSystem>().AsSingle()
            .WithArguments(_levelBounds, _bulletsParent, _activeObjectsParent, _bulletPrefab, _initBulletsCount)
            .NonLazy();*/
    }

    public void AddSMTH()
    {
        
    }
}