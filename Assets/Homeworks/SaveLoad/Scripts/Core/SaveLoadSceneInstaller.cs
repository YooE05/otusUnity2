using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoadSceneInstaller : MonoInstaller
    {
        [SerializeField] private ResourcesHandler _resourcesHandler;

        //  [SerializeField] private List<CharacterStatData> _initStats;
        //[SerializeField] private int _increaseStatPercent = 20;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameRepository>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerResourcesSaver>().AsSingle().NonLazy();
            
            Container.Bind<ResourcesHandler>().FromInstance(_resourcesHandler).AsSingle().NonLazy();
            Container.Bind<PlayerResources>().AsSingle().NonLazy();

            //Container.Bind<UserInfo>().AsSingle().WithArguments("@YooE", _initCharacterInfo).NonLazy();
            //     Container.Bind<CharacterStatsManager>().AsSingle().WithArguments(_initStats, _increaseStatPercent).NonLazy();
            //     Container.Bind<PlayerLevel>().AsSingle().WithArguments(2, 100).NonLazy();
        }
    }
}