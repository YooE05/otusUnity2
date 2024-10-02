using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class PauseResumeManager : MonoBehaviour, Listeners.IInitListener, Listeners.IStartListener,
        Listeners.IFinishListener
    {
        [SerializeField] private Button _pauseButton;

        private GamecycleManager _gamecycleManager;

        [Inject]
        public void Construct(GamecycleManager gamecycleManager)
        {
            _gamecycleManager = gamecycleManager;
        }

        public void OnInit()
        {
            _pauseButton.onClick.AddListener(OnPauseClicked);
            _pauseButton.gameObject.SetActive(false);
        }

        public void OnStart()
        {
            _pauseButton.gameObject.SetActive(true);
        }

        private void OnPauseClicked()
        {
            _gamecycleManager.OnPause();
            _pauseButton.onClick.RemoveListener(OnPauseClicked);
            _pauseButton.onClick.AddListener(OnResumeClicked);
        }

        private void OnResumeClicked()
        {
            _gamecycleManager.OnResume();
            _pauseButton.onClick.RemoveListener(OnResumeClicked);
            _pauseButton.onClick.AddListener(OnPauseClicked);
        }

        public void OnFinish()
        {
            _pauseButton.gameObject.SetActive(false);
        }
    }
}