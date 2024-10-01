using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Test _testClass;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            Container.Bind<Test>().FromInstance(_testClass).AsSingle().NonLazy();
        }
    }
}