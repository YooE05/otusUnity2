using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent: Listeners.IFixUpdaterListener
    {
        public delegate void FireHandler(Vector2 position, Vector2 direction);
        public event FireHandler OnFire;

        private bool _canFire;
        private GameObject _target;
        private float _currentTime;

        private readonly WeaponComponent _weaponComponent;
        private readonly float _countdown;

        public EnemyAttackAgent(WeaponComponent weaponComponent, float countdown)
        {
            _weaponComponent = weaponComponent;
            _countdown = countdown;
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void Reset()
        {
            _currentTime = _countdown;
            _canFire = false;
        }

        public void EnableFireAbility()
        {
            _canFire = true;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!_canFire)
            {
                return;
            }

            if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists)
            {
                return;
            }

            _currentTime -= deltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        private void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;

            OnFire?.Invoke(startPosition, direction);
        }
    }
}