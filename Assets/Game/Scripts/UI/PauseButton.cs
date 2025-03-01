using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private PauseScreen _pauseScreen;

        public void SetPauseScreen(PauseScreen pauseScreen)
        {
            _pauseScreen = pauseScreen;
        }

        public void SetVisibility(bool isOn)
        {
            gameObject.SetActive(isOn);

            if (isOn)
            {
                _button.onClick.AddListener(_pauseScreen.Show);
            }
            else
            {
                _button.onClick.RemoveAllListeners();
            }
        }
    }
}