using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public sealed class GameViewInitializer : ViewInitializer
    {
        [SerializeField] private PauseButton _pauseButton;

        protected override void ActionsWithUIInstance(GameObject screenInstance)
        {
            var screenComponent = screenInstance.GetComponent<PauseScreen>();
            DiContainer.Inject(screenComponent);

            _pauseButton.SetPauseScreen(screenComponent);
            _pauseButton.SetVisibility(true);
        }
    }
}