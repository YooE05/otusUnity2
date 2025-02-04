using System.Collections.Generic;
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

        private readonly List<AsyncOperationHandle> _loadedLocations = new List<AsyncOperationHandle>();
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

        private async void LoadLocation(AssetReference assetReference)
        {
            _moveController.SetMoveAbility(false);

            var locationPrefabHandle = await AddressablesHandler.LoadAssetHandle<GameObject>(assetReference);
            _loadedLocations.Add(locationPrefabHandle);
            Instantiate((GameObject) locationPrefabHandle.Result, _locationParent);

            _moveController.SetMoveAbility(true);
        }

        private void UnloadLocations()
        {
            _menuLoader.OnBackToMenu -= UnloadLocations;

            for (var i = 0; i < _triggerZones.Length; i++)
            {
                _triggerZones[i].OnPlayerEnter -= LoadLocation;
            }

            for (var i = _loadedLocations.Count - 1; i >= 0; i--)
            {
                AddressablesHandler.UnloadAssetHandler(_loadedLocations[i]);
            }
        }
    }
}