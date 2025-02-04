using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
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
            for (var i = 0; i < assets.Count(); i++)
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