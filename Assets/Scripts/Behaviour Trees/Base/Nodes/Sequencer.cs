using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Mina Pecheux // Modified by Robbie Beaumont
// Derived control-flow sequencer class.

namespace BehaviourTree
{
    public class Sequencer : Node
    {
        public Sequencer() : base()
        {
            
        }

        public Sequencer(List<Node> childNodes) : base(childNodes)
        {

        }

        public override NodeState Evaluate()
        {
            foreach (Node child in _childNodes)
            {
                switch(child.Evaluate())
                {
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.FAILURE:
                        _nodeState = NodeState.FAILURE;
                        return _nodeState;
                    case NodeState.RUNNING:
                        _nodeState = NodeState.RUNNING;
                        return _nodeState;
                    default:
                        _nodeState = NodeState.SUCCESS;
                        return _nodeState;
                }
            }
            return _nodeState;
        }
    }
}

