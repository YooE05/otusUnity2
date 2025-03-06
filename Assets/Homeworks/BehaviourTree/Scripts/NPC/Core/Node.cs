using System;
using System.Collections.Generic;

namespace Homeworks.BehaviourTree
{
    public enum NodeState
    {
        RUNNING = 0,
        SUCCESS = 1,
        FAILURE = 2
    }

    public class Node
    {
        protected NodeState _state;
        protected Node _parent;
        protected readonly List<Node> _children = new();

        protected Node()
        {
            _parent = null;
        }

        protected Node(List<Node> children)
        {
            for (var i = 0; i < children.Count; i++)
            {
                Attach(children[i]);
            }
        }

        private void Attach(Node node)
        {
            node._parent = this;
            _children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}