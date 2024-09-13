using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 50;

        [SerializeField] private Transform _container;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly Queue<Bullet> _bulletPool = new Queue<Bullet>();
        private readonly HashSet<Bullet> _activeBullets = new HashSet<Bullet>();
        private readonly List<Bullet> _cache = new List<Bullet>();

        private void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = Instantiate(_prefab, _container);

                _bulletPool.Enqueue(bullet);
            }
        }

        private void FixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!_levelBounds.InBounds(bullet.GetCurrentPosition()))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByConfig(BulletConfig config, Vector3 position, Vector2 direction)
        {
            FlyBulletByArgs(new BulletSystem.Args
            {
                PhysicsLayer = (int) config.PhysicsLayer,
                Color = config.Color,
                Damage = config.Damage,
                Position = position,
                Velocity = direction * config.Speed
            });
        }

        private void FlyBulletByArgs(Args args)
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.SetParent(_worldTransform);
            }
            else
            {
                bullet = Instantiate(_prefab, _worldTransform);
            }

            bullet.SetUpByArgs(args);

            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bullet.Damage);
            }

            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (!_activeBullets.Remove(bullet)) return;

            bullet.OnCollisionEntered -= OnBulletCollision;
            bullet.SetParent(_container);

            _bulletPool.Enqueue(bullet);
        }

        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
        }
    }
}