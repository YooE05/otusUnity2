using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private Transform _activeObjectsParent;

        [Header("UI")] 
        [SerializeField] private int _prestartCountdown = 3;
        [SerializeField] private StartUIView _startUIView;
        [SerializeField] private PauseUIView _pauseUIView;

        [Header("Character")] 
        [SerializeField] private GameObject _characterGO;
        [SerializeField] private BulletConfig _characterBulletConfig;
        private MoveComponent _characterMoveComponent;
        private HitPointsComponent _characterHpComponent;
        private WeaponComponent _characterWeaponComponent;

        [Header("Enemy")] 
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private BulletConfig _enemyBulletConfig;
        [SerializeField] private float _spawnDelay = 3f;

        [Header("Enemies Pool")] 
        [SerializeField] private Transform _enemiesParent;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _initEnemiesCount;

        [Header("Bullets Pool")] 
        [SerializeField] private Transform _bulletsParent;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _initBulletsCount;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GamecycleManager>().AsSingle().NonLazy();
            Container.Bind<LevelBounds>().FromInstance(_levelBounds).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletSystem>().AsSingle()
                .WithArguments(_levelBounds, _bulletsParent, _activeObjectsParent, _bulletPrefab, _initBulletsCount)
                .NonLazy();

            BindUI();
            BindInput();
            BindCharacter();
            BindEnemySystem();
        }

        private void BindUI()
        {
            Container.BindInterfacesTo<GameStartManager>().AsSingle()
                .WithArguments(_startUIView.StartButton, _startUIView.CountDownText, _prestartCountdown).NonLazy();
            Container.BindInterfacesTo<PauseResumeManager>().AsSingle()
                .WithArguments(_pauseUIView.PauseButton).NonLazy();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<FireInputManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MovementInputManager>().AsSingle().NonLazy();
        }

        private void BindCharacter()
        {
            _characterMoveComponent = _characterGO.GetComponent<MoveComponent>();
            _characterWeaponComponent = _characterGO.GetComponent<WeaponComponent>();
            _characterHpComponent = _characterGO.GetComponent<HitPointsComponent>();

            Container.BindInterfacesTo<CharacterDeathObserver>().AsSingle().WithArguments(_characterHpComponent)
                .NonLazy();
            Container.BindInterfacesTo<CharacterMovementController>().AsSingle()
                .WithArguments(_characterMoveComponent).NonLazy();
            Container.BindInterfacesTo<CharacterAttackController>().AsSingle()
                .WithArguments(_characterWeaponComponent, _characterBulletConfig).NonLazy();
        }

        private void BindEnemySystem()
        {
            Container.BindInterfacesAndSelfTo<EnemyPool>().AsSingle()
                .WithArguments(_enemiesParent, _activeObjectsParent, _enemyPrefab, _initEnemiesCount)
                .NonLazy();
            Container.BindInterfacesTo<EnemyManager>().AsSingle()
                .WithArguments(_enemyPositions, _characterGO, _enemyBulletConfig, _spawnDelay).NonLazy();
        }
    }
}