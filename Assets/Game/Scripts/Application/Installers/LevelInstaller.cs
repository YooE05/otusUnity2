using Zenject;

namespace Homework
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            /*Container.BindInterfacesAndSelfTo<GamecycleManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameStartManager>().AsSingle()
                .WithArguments(_startUIView.StartButton, _startUIView.CountDownText, _prestartCountdown).NonLazy();*/
        }
    }
}