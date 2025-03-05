using System.Collections.Generic;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class NpcBT : Tree
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private Animator _animator;

        public const float Speed = 2f;


        protected override Node SetupTree()
        {
            //Node root = new TaskPatrol(transform, _waypoints, _animator);

            Node root = new Selector(new List<Node>
            {
                //new Sequence(new List<Node>
                //{
                //    new CheckEnemyInAttackRange(transform),
                //    new TaskAttack(transform),
                //}),
                new Sequence(new List<Node>
                {
                    new CheckTreeAvailability(_animator),
                    new TaskGoToTree(transform),
                }),
                new TaskPatrol(transform, _waypoints, _animator),
            });

            return root;
        }
    }
}