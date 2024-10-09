using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterAttackController : Listeners.IFixUpdaterListener
    {
        private readonly BulletConfig _bulletConfig;
        private readonly WeaponComponent _weaponComponent;
        private readonly FireInputManager _fireInputManager;
        private readonly BulletSystem _bulletSystem;

        public CharacterAttackController(BulletSystem bulletSystem, FireInputManager fireInputManager, BulletConfig bulletConfig, WeaponComponent weaponComponent)
        {
            _bulletSystem = bulletSystem;
            _fireInputManager = fireInputManager;
            _bulletConfig = bulletConfig;
            _weaponComponent = weaponComponent;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!_fireInputManager.IsFireButtonPressed) return;

            OnFlyBullet();
            _fireInputManager.ResetFireButtonPress();
        }

        private void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByConfig(_bulletConfig, _weaponComponent.Position,
                _weaponComponent.Rotation * Vector3.up);
        }
    }
}