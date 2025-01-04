using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoadSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerResourcesHandler _playerResourcesHandler;
        [SerializeField] private EntitiesContainer _entitiesContainer;
     
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameRepository>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<PlayerResourcesSaver>().AsSingle().NonLazy();
            Container.Bind<PlayerResourcesHandler>().FromInstance(_playerResourcesHandler).AsSingle().NonLazy();
            Container.Bind<PlayerResources>().AsSingle().NonLazy();

            Container.Bind<EntitiesContainer>().FromInstance(_entitiesContainer).AsSingle().NonLazy();
            
            Container.BindInterfacesTo<UnitsDataSaver>().AsSingle().NonLazy();
            Container.Bind<UnitsHandler>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<MapResoucesSaver>().AsSingle().NonLazy();
            Container.Bind<MapResourcesHandler>().AsSingle().NonLazy();
        }
    }
}