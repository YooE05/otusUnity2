using Zenject;

namespace ShootEmUp
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GamecycleManager>().AsSingle().NonLazy();
        }
    }
}