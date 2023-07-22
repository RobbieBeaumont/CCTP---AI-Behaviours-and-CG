using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont
// Derived decorator node inverter.

namespace BehaviourTree
{
    public class Inverter : Node
    {
        public Inverter(Node childNode) : base(childNode)
        {

        }

        public override NodeState Evaluate()
        {
            foreach (Node child in _childNodes)
            {
                switch(child.Evaluate())
                {
                    case NodeState.SUCCESS:
                        _nodeState = NodeState.FAILURE;
                        continue;
                    case NodeState.FAILURE:
                        _nodeState = NodeState.SUCCESS;
                        continue;
                    case NodeState.RUNNING:
                        _nodeState = NodeState.RUNNING;
                        continue;
                }
            }
            return _nodeState;
        }
    }
}


