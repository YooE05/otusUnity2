using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterAttackController: MonoBehaviour, Listeners.IFixUpdaterListener
    {
        [SerializeField] private FireInputManager _fireInputManager;
        
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private WeaponComponent _weaponComponent;

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