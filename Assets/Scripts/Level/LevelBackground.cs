using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        private float _startPositionY;
        private float _endPositionY;

        private float _movingSpeedY;

        private float _positionX;
        private float _positionZ;

        [SerializeField] private Params _params;

        private void Awake()
        {
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
            
            var position = transform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (transform.position.y <= _endPositionY)
            {
                transform.position = new Vector3(_positionX, _startPositionY, _positionZ);
            }

            transform.position -= new Vector3(_positionX, _movingSpeedY * Time.fixedDeltaTime, _positionZ);
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float StartPositionY;

            [SerializeField] public float EndPositionY;

            [SerializeField] public float MovingSpeedY;
        }
    }
}