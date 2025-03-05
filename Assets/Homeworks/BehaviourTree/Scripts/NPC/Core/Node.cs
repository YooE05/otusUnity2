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

        public Node Parent;
        protected List<Node> _children = new();

        private Dictionary<string, object> _dataContext = new();

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> children)
        {
            for (var i = 0; i < children.Count; i++)
            {
                Attach(children[i]);
            }
        }

        private void Attach(Node node)
        {
            node.Parent = this;
            _children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            var node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }

                node = node.Parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            var node = Parent;
            while (node != null)
            {
                var cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }

                node = node.Parent;
            }

            return false;
        }
    }
}