using UnityEngine;
using Zenject;

namespace Homework.Inventory
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private HeroStats _hero;
        
        public override void InstallBindings()
        {
            Container.Bind<Inventory>().AsSingle().NonLazy();
            
            Container.Bind<HeroStats>().FromInstance(_hero).AsSingle().NonLazy();
            Container.BindInterfacesTo<DamageEffectObserver>().AsSingle().NonLazy();
            
            /*Container.BindInterfacesAndSelfTo<GamecycleManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameStartManager>().AsSingle()
                .WithArguments(_startUIView.StartButton, _startUIView.CountDownText, _prestartCountdown).NonLazy();*/
        }
    }
}