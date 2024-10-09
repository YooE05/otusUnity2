using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : Listeners.IStartListener, Listeners.IFinishListener
    {
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly GamecycleManager _gamecycleManager;

        public CharacterDeathObserver(HitPointsComponent hitPointsComponent, GamecycleManager gamecycleManager)
        {
            _hitPointsComponent = hitPointsComponent;
            _gamecycleManager = gamecycleManager;
        }

        public void OnStart()
        {
            _hitPointsComponent.OnHpEmpty += OnCharacterDeath;
        }

        public void OnFinish()
        {
            _hitPointsComponent.OnHpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => _gamecycleManager.OnFinish();
    }
}