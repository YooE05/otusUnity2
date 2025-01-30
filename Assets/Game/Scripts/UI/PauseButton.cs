using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        private void OnEnable()
        {
            _button.onClick.AddListener(_pauseScreen.Show);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_pauseScreen.Show);
        }
    }
}