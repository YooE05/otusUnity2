using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
       
        [Inject]
        private GameManager _gameManager;
        
        private void OnEnable()
        {
            _hitPointsComponent.OnHpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _hitPointsComponent.OnHpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => _gameManager.OnFinish();
    }
}