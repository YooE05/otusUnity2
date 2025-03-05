using Homeworks.UpgradeManager;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class Blackboard : MonoBehaviour
    {
        [SerializeField] private Resource _treeResource;
        public static Resource TreeResource { get; private set; }
        public static bool IsTreeAvailable { get; private set; }
        public static Transform TreeTransform { get; private set; }

        [SerializeField] private StationHandler _converter;
        public static StationHandler Converter;
        public static Transform ConverterStayPoint { get; private set; }
        [SerializeField] private Transform _converterStayPoint;
        
        private void Awake()
        {
            TreeTransform = _treeResource.transform;
            ConverterStayPoint = _converterStayPoint;
            Converter = _converter;
        }

        private void Update()
        {
            IsTreeAvailable = _treeResource.IsReadyToGet;
            TreeResource = _treeResource;
        }
    }
}