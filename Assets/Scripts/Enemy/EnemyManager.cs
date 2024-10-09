using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : Listeners.IStartListener, Listeners.IFixUpdaterListener,
        Listeners.IFinishListener
    {
        private float _currentTime;
        private bool _spawnIsEnable;

        private Dictionary<GameObject, EnemyAttackAgent> _attackAgentsDictionary = new Dictionary<GameObject, EnemyAttackAgent>();
        private Dictionary<GameObject, EnemyMoveAgent> _moveAgentsDictionary = new Dictionary<GameObject, EnemyMoveAgent>();

        private readonly float _spawnDelay;
        private readonly float _fireDelay;
        private readonly GamecycleManager _gamecycleManager;
        private readonly EnemyPositions _enemyPositions;
        private readonly GameObject _character;
        private readonly BulletConfig _bulletConfig;
        private readonly EnemyPool _enemyPool;
        private readonly BulletSystem _bulletSystem;

        public EnemyManager(GamecycleManager gamecycleManager, BulletSystem bulletSystem, EnemyPool enemyPool,
            EnemyPositions enemyPositions,
            GameObject character, BulletConfig bulletConfig, float spawnDelay = 3f, float fireDelay = 2f)
        {
            _gamecycleManager = gamecycleManager;
            _enemyPool = enemyPool;
            _bulletSystem = bulletSystem;
            _enemyPositions = enemyPositions;
            _character = character;
            _bulletConfig = bulletConfig;
            _spawnDelay = spawnDelay;
            _fireDelay = fireDelay;

            _spawnIsEnable = false;
            _currentTime = 0f;
        }

        public void OnStart()
        {
            _spawnIsEnable = true;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!_spawnIsEnable) return;

            _currentTime += deltaTime;
            if (!(_currentTime >= _spawnDelay)) return;

            SpawnEnemy();
            _currentTime = 0f;
        }

        public void OnFinish()
        {
            _spawnIsEnable = false;
        }

        private void SpawnEnemy()
        {
            var enemy = _enemyPool.SpawnEnemy();

            if (enemy == null) return;

            SetUpEnemy(enemy);
        }

        private void SetUpEnemy(GameObject enemy)
        {
            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            enemy.GetComponent<HitPointsComponent>().OnHpEmpty += OnDestroyed;

            if (!_attackAgentsDictionary.ContainsKey(enemy))
            {
                var newAttackAgent = new EnemyAttackAgent(enemy.GetComponent<WeaponComponent>(), _fireDelay);
                _gamecycleManager.AddListener(newAttackAgent);
                _attackAgentsDictionary.Add(enemy, newAttackAgent);
            }
            _attackAgentsDictionary[enemy].SetTarget(_character);
            _attackAgentsDictionary[enemy].OnFire += OnFire;

            if (!_moveAgentsDictionary.ContainsKey(enemy))
            {
                var newMoveAgent = new EnemyMoveAgent(enemy.GetComponent<MoveComponent>(), enemy.transform); 
                _gamecycleManager.AddListener(newMoveAgent);
                _moveAgentsDictionary.Add(enemy, newMoveAgent);
            }
            var attackPosition = _enemyPositions.RandomAttackPosition();
            _moveAgentsDictionary[enemy].SetDestination(attackPosition.position);
            _moveAgentsDictionary[enemy].OnDestinationReached +=
                _attackAgentsDictionary[enemy].EnableFireAbility;
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= OnDestroyed;
            _attackAgentsDictionary[enemy].OnFire -= OnFire;
            _attackAgentsDictionary[enemy].Reset();
            _moveAgentsDictionary[enemy].OnDestinationReached -=
                _attackAgentsDictionary[enemy].EnableFireAbility;

            _enemyPool.UnspawnEnemy(enemy);
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByConfig(_bulletConfig, position, direction);
        }
    }
}