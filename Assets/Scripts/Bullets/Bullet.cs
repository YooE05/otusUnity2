using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        
        [NonSerialized] public int Damage;
        [SerializeField] private new Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetUpByArgs(BulletSystem.Args args)
        {
            SetPosition(args.Position);
            SetColor(args.Color);
            SetPhysicsLayer(args.PhysicsLayer);
            Damage = args.Damage;
            SetVelocity(args.Velocity);
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }

        private void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}