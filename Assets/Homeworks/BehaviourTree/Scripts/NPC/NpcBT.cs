using System.Collections.Generic;
using Homeworks.UpgradeManager;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class NpcBT : Tree
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speed = 4f;
        [SerializeField] private float _getResourcesRange = 2f;

        [SerializeField] private StationHandler _converter;
        [SerializeField] private Transform _converterStayPoint;

        [SerializeField] private ResourcesSpawner _resourcesSpawner;

        private readonly ResourcesContainer _resourcesContainer = new();
        public Resource TargetResource;
        
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckTreeInRange(transform, _animator, _getResourcesRange, this),
                    new TaskResourceExtraction(_animator, _resourcesContainer, this),
                }),
                new Sequence(new List<Node>
                {
                    new CheckTreeAvailability(_animator, _resourcesSpawner),
                    new TaskGoToTree(transform, _speed, _resourcesSpawner, this),
                }),
                new Sequence(new List<Node>
                {
                    new CheckHasResources(_resourcesContainer),
                    new TaskGoToConverter(transform, _speed, _converterStayPoint),
                    new TaskPutResources(_animator, _converter, _resourcesContainer),
                }),
                new TaskPatrol(transform, _waypoints, _animator, _speed),
            });

            return root;
        }
    }
}