using UniRx;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class UIAssetLoadObserver : MonoBehaviour
    {
        [SerializeField] private ViewInitializer _viewInitializer;
        private UiAssetsContainer _uiAssetsContainer;

        [Inject]
        public void Construct(UiAssetsContainer uiAssetsContainer)
        {
            _uiAssetsContainer = uiAssetsContainer;

            if (_uiAssetsContainer.AllAssetsLoaded.Value == true)
            {
                LoadAssets();
            }
            else
            {
                _uiAssetsContainer.AllAssetsLoaded.Subscribe(delegate { LoadAssets(); }).AddTo(this);
            }
        }

        private void LoadAssets()
        {
            if (_uiAssetsContainer.AllAssetsLoaded.Value == true)
            {
                _viewInitializer.Init(_uiAssetsContainer);
            }
        }
    }
}