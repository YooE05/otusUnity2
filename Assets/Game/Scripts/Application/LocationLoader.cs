using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public class LocationLoader : MonoBehaviour
    {
        [SerializeField] private Transform _locationParent;
        [SerializeField] private TriggerZone[] _triggerZones;

        private readonly List<GameObject> _loadedLocations = new List<GameObject>();
        private MoveController _moveController;
        private MenuLoader _menuLoader;

        [Inject]
        public void Construct(MoveController moveController, MenuLoader menuLoader)
        {
            _menuLoader = menuLoader;
            _menuLoader.OnBackToMenu += UnloadLocations;

            _moveController = moveController;
            for (int i = 0; i < _triggerZones.Length; i++)
            {
                _triggerZones[i].OnPlayerEnter += LoadLocation;
            }
        }

        private async void LoadLocation(AssetReference assetReference)
        {
            //остановить ввод игрока
            _moveController.SetMoveAbility(false);

            //загрузить ассет
            var locationPrefab = await AddressablesHandler.LoadAsset<GameObject>(assetReference);
            var locationGO = Instantiate(locationPrefab, _locationParent);

            //захешировать объект
            _loadedLocations.Add(locationPrefab);

            //восстановить ввод игрока
            _moveController.SetMoveAbility(true);
        }

        private void UnloadLocations()
        {
            _menuLoader.OnBackToMenu -= UnloadLocations;

            for (int i = 0; i < _triggerZones.Length; i++)
            {
                _triggerZones[i].OnPlayerEnter -= LoadLocation;
            }

            for (int i = 0; i < _loadedLocations.Count; i++)
            {
                AddressablesHandler.UnloadAsset(_loadedLocations[i]);
            }
        }
    }
}