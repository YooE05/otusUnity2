using UnityEngine;

namespace ShootEmUp
{
    public sealed class MovementInputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }
    }
}