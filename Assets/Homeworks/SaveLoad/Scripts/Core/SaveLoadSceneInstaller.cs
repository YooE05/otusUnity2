using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoadSceneInstaller : MonoInstaller
    {
        //[SerializeField] private CharacterInfoData _initCharacterInfo;

        //  [SerializeField] private List<CharacterStatData> _initStats;
        //[SerializeField] private int _increaseStatPercent = 20;

        public override void InstallBindings()
        {
            Container.Bind<GameRepository>().AsSingle().NonLazy();
            

            //Container.Bind<UserInfo>().AsSingle().WithArguments("@YooE", _initCharacterInfo).NonLazy();
            //     Container.Bind<CharacterStatsManager>().AsSingle().WithArguments(_initStats, _increaseStatPercent).NonLazy();
            //     Container.Bind<PlayerLevel>().AsSingle().WithArguments(2, 100).NonLazy();
        }
    }
}