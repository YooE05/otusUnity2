using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private GameObject _resourceView;

        public Transform Transform => _resourceView.transform;
        public bool IsReadyToGet { get; private set; } = false;

        [Button]
        public void Enable()
        {
            IsReadyToGet = true;
            _resourceView.gameObject.SetActive(true);
        }

        public void Get()
        {
            IsReadyToGet = false;
            _resourceView.gameObject.SetActive(false);
        }
    }
}