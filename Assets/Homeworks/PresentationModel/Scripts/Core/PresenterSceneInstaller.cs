using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PresenterSceneInstaller : MonoInstaller
    {
        [SerializeField] private CharacterInfoData _initCharacterInfo;
        [SerializeField] private List<CharacterStatData> _initStats;


        public override void InstallBindings()
        {
            Container.Bind<UserInfo>().AsSingle().WithArguments("@YooE", _initCharacterInfo).NonLazy();
            Container.Bind<CharacterStatsManager>().AsSingle().WithArguments(_initStats).NonLazy();

            /*Container.BindInterfacesAndSelfTo<GamecycleManager>().AsSingle().NonLazy();
            Container.Bind<LevelBounds>().FromInstance(_levelBounds).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletSystem>().AsSingle()
                .WithArguments(_levelBounds, _bulletsParent, _activeObjectsParent, _bulletPrefab, _initBulletsCount)
                .NonLazy();*/
        }

        public void AddSMTH()
        {
        }
    }
}