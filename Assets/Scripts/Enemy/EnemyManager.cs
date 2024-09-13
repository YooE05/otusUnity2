using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        private readonly HashSet<GameObject> _activeEnemies = new HashSet<GameObject>();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = _enemyPool.SpawnEnemy();

                if (enemy == null) continue;
                if (!_activeEnemies.Add(enemy)) continue;

                enemy.GetComponent<HitPointsComponent>().OnHpEmpty += OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
                enemy.GetComponent<EnemyMoveAgent>().OnDestinationReached +=
                    enemy.GetComponent<EnemyAttackAgent>().EnableFireAbility;
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (!_activeEnemies.Remove(enemy)) return;

            enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;
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