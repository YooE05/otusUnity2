using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public sealed class MenuViewInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _canvas;
        [SerializeField] private AssetReference _menuScreenReference;
        private UiAssetsContainer _uiAssetContainer;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(UiAssetsContainer uiAssetsContainer, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _uiAssetContainer = uiAssetsContainer;
        }

        private void Start()
        {
            var menuScreen = _uiAssetContainer.GetAsset(_menuScreenReference.AssetGUID);

            if (menuScreen)
            {
                menuScreen.SetActive(false);
                var screen = Instantiate(menuScreen, _canvas);
                _diContainer.Inject(screen.GetComponent<MenuScreen>());
                screen.SetActive(true);
                menuScreen.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Havn't get Menu Screen asset!");
            }
        }
    }
}