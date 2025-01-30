using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public sealed class GameViewInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _canvas;
        [SerializeField] private AssetReference _gameScreenReference;
        [SerializeField] private PauseButton _pauseButton;

        private UiAssetsContainer _uiAssetContainer;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(UiAssetsContainer uiAssetsContainer, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _uiAssetContainer = uiAssetsContainer;
        }

        private void Awake()
        {
            _pauseButton.gameObject.SetActive(false);
        }

        private void Start()
        {
            var screenPrefab = _uiAssetContainer.GetAsset(_gameScreenReference.AssetGUID);

            if (screenPrefab)
            {
                screenPrefab.SetActive(false);

                var screenGO = Instantiate(screenPrefab, _canvas);
                var screenComponent = screenGO.GetComponent<PauseScreen>();
                _diContainer.Inject(screenComponent);
                
                _pauseButton.SetPauseScreen(screenComponent);
                _pauseButton.gameObject.SetActive(true);
                
                screenPrefab.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Havn't get Game Pause Screen asset!");
            }
        }
    }
}