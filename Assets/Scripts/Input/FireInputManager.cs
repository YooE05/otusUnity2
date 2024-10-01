using UnityEngine;

namespace ShootEmUp
{
    public sealed class FireInputManager : MonoBehaviour, Listeners.IUpdateListener
    {
        public bool IsFireButtonPressed { get; private set; }

        public void ResetFireButtonPress()
        {
            IsFireButtonPressed = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsFireButtonPressed = true;
            }
        }
    }
}