using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont
// Derived root node class.

namespace BehaviourTree
{
    public class Root : Node
    {
        public Root(Node childNodes) : base(childNodes)
        {
            
        }

        public override NodeState Evaluate()
        {
            foreach (Node child in _childNodes)
            {
                _nodeState = child.Evaluate();
            }
            return _nodeState;
        }
    }
}

