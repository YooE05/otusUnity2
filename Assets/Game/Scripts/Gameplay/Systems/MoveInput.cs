using UnityEngine;

namespace SampleGame
{
    public sealed class MoveInput : IMoveInput
    {
        private readonly InputConfig _config;

        public MoveInput(InputConfig config)
        {
            _config = config;
        }

        public Vector3 GetDirection()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(_config.forward))
            {
                direction.z = 1;
            }
            else if (Input.GetKey(_config.back))
            {
                direction.z = -1;
            }

            if (Input.GetKey(_config.left))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(_config.right))
            {
                direction.x = 1;
            }

            return direction;
        }
    }
}