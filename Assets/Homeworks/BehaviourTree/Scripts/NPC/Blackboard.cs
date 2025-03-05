using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class Blackboard : MonoBehaviour
    {
        public static bool IsTreeAvailable { get; private set; }
        [SerializeField] private bool _isTreeAvailable;
        public static Transform TreeTransform { get; private set; }
        [SerializeField] private Transform _treeTransform;

        private void Awake()
        {
            TreeTransform = _treeTransform;
        }

        private void Update()
        {
            IsTreeAvailable = _isTreeAvailable;
        }
    }
}