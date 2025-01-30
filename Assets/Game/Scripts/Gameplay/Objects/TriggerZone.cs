using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public class TriggerZone : MonoBehaviour
    {
        public event Action<AssetReference> OnPlayerEnter;

        [SerializeField] private AssetReference _nextLocation;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Character>())
            {
                OnPlayerEnter?.Invoke(_nextLocation);
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}