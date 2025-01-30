using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class PauseScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private Button _exitButton;

        private MenuLoader _menuLoader;

        [Inject]
        public void Construct(MenuLoader menuLoader, GameLoader gameLoader)
        {
            _menuLoader = menuLoader;
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(Hide);
            _exitButton.onClick.AddListener(_menuLoader.LoadMenu);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(Hide);
            _exitButton.onClick.RemoveListener(_menuLoader.LoadMenu);
        }

        public void Show()
        {
            Time.timeScale = 0; //KISS
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            Time.timeScale = 1; //KISS
            gameObject.SetActive(false);
        }
    }
}