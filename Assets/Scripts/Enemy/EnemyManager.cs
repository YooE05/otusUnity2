using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : Listeners.IStartListener, Listeners.IFixUpdaterListener,
        Listeners.IFinishListener
    {
        private float _currentTime;
        private bool _spawnIsEnable;

        private readonly float _spawnDelay;
        private readonly EnemyPositions _enemyPositions;
        private readonly GameObject _character;
        private readonly BulletConfig _bulletConfig;
        private readonly EnemyPool _enemyPool;
        private readonly BulletSystem _bulletSystem;

        public EnemyManager(BulletSystem bulletSystem, EnemyPool enemyPool, EnemyPositions enemyPositions,
            GameObject character, BulletConfig bulletConfig, float spawnDelay)
        {
            _enemyPool = enemyPool;
            _bulletSystem = bulletSystem;
            _enemyPositions = enemyPositions;
            _character = character;
            _bulletConfig = bulletConfig;
            _spawnDelay = spawnDelay;

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

            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);
            enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;

            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyMoveAgent>().OnDestinationReached +=
                enemy.GetComponent<EnemyAttackAgent>().EnableFireAbility;
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;
            enemy.GetComponent<EnemyAttackAgent>().Reset();
            enemy.GetComponent<EnemyMoveAgent>().OnDestinationReached -=
                enemy.GetComponent<EnemyAttackAgent>().EnableFireAbility;

            _enemyPool.UnspawnEnemy(enemy);
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByConfig(_bulletConfig, position, direction);
        }
    }
}