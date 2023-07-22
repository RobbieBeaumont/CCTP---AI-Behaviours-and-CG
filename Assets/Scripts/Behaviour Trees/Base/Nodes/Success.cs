using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont
// Derived decorator node Success.

namespace BehaviourTree
{
    public class Success : Node
    {
        public Success(Node childNode) : base(childNode)
        {

        }

        public override NodeState Evaluate()
        {
            foreach (Node child in _childNodes)
            {
                child.Evaluate();
                _nodeState = NodeState.SUCCESS;
            }
            return _nodeState;
        }
    }
}

