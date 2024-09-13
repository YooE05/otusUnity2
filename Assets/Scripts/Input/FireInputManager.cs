using UnityEngine;

namespace ShootEmUp
{
    public sealed class FireInputManager : MonoBehaviour
    {
        public bool IsFireButtonPressed { get; private set; }

        public void ResetFireButtonPress()
        {
            IsFireButtonPressed = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsFireButtonPressed = true;
            }
        }
    }
}