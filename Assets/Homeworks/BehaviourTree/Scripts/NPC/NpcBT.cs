using System.Collections.Generic;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class NpcBT : Tree
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private Animator _animator;

        public static readonly ResourcesContainer ResourcesContainer = new();

        public const float Speed = 4f;
        public const float GetResourcesRange = 2f;

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckTreeInRange(transform, _animator),
                    new TaskResourceExtraction(_animator),
                }),
                new Sequence(new List<Node>
                {
                    new CheckTreeAvailability(_animator),
                    new TaskGoToTree(transform),
                }),
                new Sequence(new List<Node>
                {
                    new CheckHasResources(),
                    new TaskGoToConverter(transform),
                    new TaskPutResources(_animator),
                }),
                new TaskPatrol(transform, _waypoints, _animator),
            });

            return root;
        }
    }
}