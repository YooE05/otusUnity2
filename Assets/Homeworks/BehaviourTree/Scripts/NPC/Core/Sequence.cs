using System.Collections.Generic;

namespace Homeworks.BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence() : base()
        {
        }

        public Sequence(List<Node> children) : base(children)
        {
        }

        public override NodeState Evaluate()
        {
            foreach (var node in _children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        _state = NodeState.FAILURE;
                        return _state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        _state = NodeState.RUNNING;
                        return _state;
                    default:
                        _state = NodeState.SUCCESS;
                        return _state;
                }
            }

            _state = NodeState.SUCCESS;
            return _state;
        }
    }
}