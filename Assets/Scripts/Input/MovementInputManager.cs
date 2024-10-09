using UnityEngine;

namespace ShootEmUp
{
    public sealed class MovementInputManager : Listeners.IUpdateListener
    {
        public float HorizontalDirection { get; private set; }

        public void OnUpdate(float deltaTime)
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