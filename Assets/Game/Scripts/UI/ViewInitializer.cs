using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public class ViewInitializer : MonoBehaviour
    {
        [SerializeField] protected Transform _canvas;
        [SerializeField] private AssetReference _menuScreenReference;
        protected DiContainer DiContainer;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }

        public void Init(UiAssetsContainer uiAssetsContainer)
        {
            var screenPrefab = uiAssetsContainer.GetAsset(_menuScreenReference.AssetGUID);

            if (screenPrefab != null)
            {
                screenPrefab.SetActive(false);

                var screenInstance = Instantiate(screenPrefab, _canvas);
                ActionsWithUIInstance(screenInstance);

                screenPrefab.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Havn't get {_menuScreenReference.SubObjectName} asset!");
            }
        }

        protected virtual void ActionsWithUIInstance(GameObject screenInstance)
        {
        }
    }
}