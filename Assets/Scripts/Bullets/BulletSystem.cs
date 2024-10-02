using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : Pool<Bullet>, Listeners.IFixUpdaterListener, Listeners.IFinishListener
    {
        [SerializeField] private LevelBounds _levelBounds;

        private readonly List<Bullet> _cache = new List<Bullet>();

        public void OnFixedUpdate(float deltaTime)
        {
            CheckBulletsOutOfBounds();
        }

        public void OnFinish()
        {
            _cache.Clear();
            _cache.AddRange(GetActive());
            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                RemoveBullet(_cache[i]);
            }
        }

        private void CheckBulletsOutOfBounds()
        {
            _cache.Clear();
            _cache.AddRange(GetActive());

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
            var bullet = Get();
            bullet.SetParent(_releasedParent);
            bullet.SetUpByArgs(args);
            bullet.OnCollisionEntered += OnBulletCollision;
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
            bullet.SetParent(_parent);
            bullet.OnCollisionEntered -= OnBulletCollision;
            Return(bullet);
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