using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public sealed class TriggerZone : MonoBehaviour
    {
        public event Action<AssetReference> OnPlayerEnter;

        [SerializeField] private AssetReference _nextLocation;
        [SerializeField] private Collider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Character>(out var character))
            {
                OnPlayerEnter?.Invoke(_nextLocation);
                _collider.enabled = false;
            }
        }
    }
}