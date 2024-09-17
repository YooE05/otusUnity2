using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : Pool<GameObject>
    {
        public GameObject SpawnEnemy()
        {
            if (GetActive().Count >= _initCount)
                return null;

            var enemy = Get();
            enemy.transform.SetParent(_releasedParent);
            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_parent);
            Return(enemy);
        }
    }
}