using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SampleGame
{
    public class LocationLoader : MonoBehaviour
    {
        [SerializeField] private Transform _locationParent;
        [SerializeField] private TriggerZone[] _triggerZones;

        private readonly List<AsyncOperationHandle> _loadedLocations = new();
        private MoveController _moveController;
        private MenuLoader _menuLoader;

        [Inject]
        public void Construct(MoveController moveController, MenuLoader menuLoader)
        {
            _moveController = moveController;
            for (var i = 0; i < _triggerZones.Length; i++)
            {
                _triggerZones[i].OnPlayerEnter += LoadLocation;
            }

            _menuLoader = menuLoader;
            _menuLoader.OnBackToMenu += UnloadLocations;
        }

        private void LoadLocation(AssetReference assetReference)
        {
            LoadLocationAsync(assetReference).Forget();
        }

        private async UniTaskVoid LoadLocationAsync(AssetReference assetReference)
        {
            _moveController.SetMoveAbility(false);

            var locationPrefabHandle = assetReference.LoadAssetAsync<GameObject>();
            await locationPrefabHandle;

            _loadedLocations.Add(locationPrefabHandle);
            Instantiate(locationPrefabHandle.Result, _locationParent);

            _moveController.SetMoveAbility(true);
        }

        private void UnloadLocations()
        {
            _menuLoader.OnBackToMenu -= UnloadLocations;

            for (var i = 0; i < _triggerZones.Length; i++)
            {
                _triggerZones[i].OnPlayerEnter -= LoadLocation;
            }

            for (var i = 0; i < _loadedLocations.Count; i++)
            {
                Addressables.Release(_loadedLocations[i]);
            }
        }
    }
}