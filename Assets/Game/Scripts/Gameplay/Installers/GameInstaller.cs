using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraConfig _cameraConfig;

        [SerializeField] private new Camera _camera;

        [SerializeField] private InputConfig _inputConfig;

        public override void InstallBindings()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_camera);

            Container
                .Bind<ICharacter>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<MoveController>()
                .AsCached()
                .NonLazy();

            Container
                .Bind<IMoveInput>()
                .To<MoveInput>()
                .AsSingle()
                .WithArguments(_inputConfig)
                .NonLazy();

            Container
                .BindInterfacesTo<CameraFollower>()
                .AsCached()
                .WithArguments(_cameraConfig._cameraOffset)
                .NonLazy();
        }
    }
}