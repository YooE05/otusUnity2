using UnityEngine.UI;

namespace ShootEmUp
{
    public class PauseResumeManager : Listeners.IInitListener, Listeners.IStartListener,
        Listeners.IFinishListener
    {
        private readonly Button _pauseButton;
        private readonly GamecycleManager _gamecycleManager;

        public PauseResumeManager(GamecycleManager gamecycleManager, Button pauseButton)
        {
            _gamecycleManager = gamecycleManager;
            _pauseButton = pauseButton;
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