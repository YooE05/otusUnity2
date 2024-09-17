using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private GameObject _character;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        private void Start()
        {
            StartCoroutine(SpawnEnemiesByDelay());
        }

        private IEnumerator SpawnEnemiesByDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);
                SpawnEnemy();
            }
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