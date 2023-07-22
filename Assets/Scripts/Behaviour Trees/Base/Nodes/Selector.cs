using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Mina Pecheux
// Derived Control-Flow Selector class.

namespace BehaviourTree
{
    public class Selector : Node
    {
        public Selector() : base()
        {

        }

        public Selector(List<Node> childNodes) : base(childNodes)
        {

        }

        public override NodeState Evaluate()
        {
            foreach (Node child in _childNodes)
            {
                switch(child.Evaluate())
                {
                    case NodeState.SUCCESS:
                        _nodeState = NodeState.SUCCESS;
                        return _nodeState;
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.RUNNING:
                        _nodeState = NodeState.RUNNING;
                        return _nodeState;
                }
            }
            _nodeState = NodeState.FAILURE;
            return _nodeState;
        }
    }
}


