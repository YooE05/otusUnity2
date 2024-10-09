using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, Listeners.IInitListener, Listeners.IFixUpdaterListener
    {
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        private float _positionX;
        private float _positionZ;

        public void OnInit()
        {
            var position = transform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (transform.position.y <= _endPositionY)
            {
                transform.position = new Vector3(_positionX, _startPositionY, _positionZ);
            }

            transform.position -= new Vector3(_positionX, _movingSpeedY * Time.fixedDeltaTime, _positionZ);
        }
    }
}