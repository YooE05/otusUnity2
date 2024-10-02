using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class GameStartManager : MonoBehaviour
    {
        private GamecycleManager _gamecycleManager;

        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _countDownText;
        [SerializeField] private int _countDownTime = 3;

        [Inject]
        public void Construct(GamecycleManager gamecycleManager)
        {
            _gamecycleManager = gamecycleManager;
            _startButton.onClick.AddListener(OnStartGame);
            _countDownText.text = string.Empty;
        }

        private void OnStartGame()
        {
            _startButton.gameObject.SetActive(false);
            _startButton.onClick.RemoveListener(OnStartGame);

            StartCoroutine(CountdownStart());
        }

        private IEnumerator CountdownStart()
        {
            for (int i = 0; i < _countDownTime; i++)
            {
                _countDownText.text = (_countDownTime - i).ToString();
                yield return new WaitForSeconds(1f);
            }

            _gamecycleManager.OnStart();

            _countDownText.text = string.Empty;
        }
    }
}