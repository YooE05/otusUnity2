using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using UniRx;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AssetReference[] _uiAssetReferences;

        public override void InstallBindings()
        {
            Container.Bind<UiAssetsContainer>().AsSingle().WithArguments(_uiAssetReferences).NonLazy();

            Container.Bind<ApplicationExiter>().AsSingle().NonLazy();
            Container.Bind<GameLoader>().AsSingle().NonLazy();
            Container.Bind<MenuLoader>().AsSingle().NonLazy();
        }
    }

    public sealed class UiAssetsContainer
    {
        public readonly BoolReactiveProperty AllAssetsLoaded = new BoolReactiveProperty();
        private readonly Dictionary<string, GameObject> _assetDictionary = new Dictionary<string, GameObject>();

        public UiAssetsContainer(AssetReference[] assets)
        {
            InitDictionary(assets).Forget();
        }

        private async UniTask InitDictionary(AssetReference[] assets)
        {
            AllAssetsLoaded.Value = false;
            for (int i = 0; i < assets.Count(); i++)
            {
                var uiScreen = await AddressablesHandler.LoadAsset<GameObject>(assets[i]);
                _assetDictionary.Add(assets[i].AssetGUID, uiScreen);
            }

            AllAssetsLoaded.Value = true;
        }

        public GameObject GetAsset(string assetId)
        {
            return _assetDictionary.ContainsKey(assetId) ? _assetDictionary[assetId] : default;
        }
    }
}