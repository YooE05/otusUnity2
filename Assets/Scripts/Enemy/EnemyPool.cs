using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : Pool<GameObject>
    {
        public EnemyPool(Transform parent, Transform releasedParent, GameObject prefab,
            int initCount)
        {
            _parent = parent;
            _releasedParent = releasedParent;
            _prefab = prefab;
            _initCount = initCount;
        }

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