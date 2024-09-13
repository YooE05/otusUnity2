using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        private float startPositionY;
        private float endPositionY;

        private float movingSpeedY;

        private float positionX;
        private float positionZ;

        [SerializeField] private Params _params;

        private void Awake()
        {
            startPositionY = _params.StartPositionY;
            endPositionY = _params.EndPositionY;
            movingSpeedY = _params.MovingSpeedY;
            
            var position = transform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (transform.position.y <= endPositionY)
            {
                transform.position = new Vector3(positionX, startPositionY, positionZ);
            }

            transform.position -= new Vector3(positionX, movingSpeedY * Time.fixedDeltaTime, positionZ);
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