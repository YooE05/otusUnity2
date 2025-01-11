using UnityEngine;
using Zenject;
using GameEngine;

namespace Homeworks.SaveLoad
{
    public class SaveLoadSceneInstaller : MonoInstaller
    {
        [SerializeField] private DataSaveConfig _saveConfig;
       
        [SerializeField] private PlayerStorageHandler _playerStorageHandler;

        [SerializeField] private UnitManager _unitManager;
        [SerializeField] private ResourceService _resourceService;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FileDataStreamer>().AsSingle().WithArguments(_saveConfig).NonLazy();
            Container.BindInterfacesTo<GameRepository>().AsSingle().NonLazy();

            Container.BindInterfacesTo<UnitsDataSaveLoader>().AsSingle().NonLazy();
            Container.Bind<UnitManager>().FromInstance(_unitManager).AsSingle().NonLazy();
            _unitManager.SetupUnits(FindObjectsOfType<Unit>());

            Container.Bind<ResourceService>().FromInstance(_resourceService).AsSingle().NonLazy();
            _resourceService.SetResources(FindObjectsOfType<Resource>());
            Container.BindInterfacesTo<ResoucesSaveLoader>().AsSingle().NonLazy();

            Container.BindInterfacesTo<PlayerStorageSaveLoader>().AsSingle().NonLazy();
            Container.Bind<PlayerStorageHandler>().FromInstance(_playerStorageHandler).AsSingle().NonLazy();
            Container.Bind<PlayerResourcesStorage>().AsSingle().NonLazy();
        }
    }
}