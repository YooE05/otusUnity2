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

            BindObservers();
        }

        private void BindObservers()
        {
            Container.BindInterfacesTo<DamageEffectObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ManaEffectObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ResistanceEffectObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SpeedEffectObserver>().AsSingle().NonLazy();
        }
    }
}