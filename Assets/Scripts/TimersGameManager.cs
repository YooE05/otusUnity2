using UnityEngine;
using Zenject;

namespace Chests
{
    public class TimersGameManager : MonoBehaviour
    {
        [Inject] private ChestService _chestService;

        private void Start()
        {
            _chestService.ResetTimers();
        }

        private void Update()
        {
            _chestService.CheckTimersReady();
        }
    }
}