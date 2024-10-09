using TMPro;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class GameStartManager : Listeners.IPrestartUpdateListener
    {
        private readonly GamecycleManager _gamecycleManager;
        private readonly Button _startButton;
        private readonly TextMeshProUGUI _countdownText;

        private int _countdownTime;
        private bool _isTimerOn;
        private float _currentTime;

        public GameStartManager(GamecycleManager gamecycleManager, Button startButton, TextMeshProUGUI countdownText,
            int countdownTime = 3)
        {
            _gamecycleManager = gamecycleManager;
            _countdownTime = countdownTime;
            _startButton = startButton;
            _countdownText = countdownText;

            InitView();
        }

        private void InitView()
        {
            _startButton.onClick.AddListener(OnStartCountdown);
            _countdownText.text = string.Empty;
            _isTimerOn = false;
            _currentTime = 0f;
        }

        private void OnStartCountdown()
        {
            _startButton.gameObject.SetActive(false);
            _startButton.onClick.RemoveListener(OnStartCountdown);

            if (CheckCountdownEnd()) return;
            _countdownText.text = (_countdownTime).ToString();
            _isTimerOn = true;
        }

        public void OnPrestartUpdate(float deltaTime)
        {
            if (!_isTimerOn) return;

            _currentTime += deltaTime;
            if (_currentTime < 1f) return;

            DecreaseCountdown();
            CheckCountdownEnd();
        }

        private bool CheckCountdownEnd()
        {
            if (_countdownTime <= 0)
            {
                FinishCountdown();
                return true;
            }

            return false;
        }

        private void DecreaseCountdown()
        {
            _countdownTime--;
            _countdownText.text = (_countdownTime).ToString();
            _currentTime = 0f;
        }

        private void FinishCountdown()
        {
            _isTimerOn = false;
            _countdownText.text = string.Empty;
            _gamecycleManager.OnStart();
        }
    }
}