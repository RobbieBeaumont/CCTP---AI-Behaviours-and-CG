using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Mina Pecheux // Modified by Robbie Beaumont
// Base node class.

namespace BehaviourTree
{
    public class Node
    {
        protected NodeState _nodeState;
        public Node _parentNode;
        protected List<Node> _childNodes = new List<Node>();

        public Node()
        {
            _parentNode = null;
        }

        public Node(Node childNode)
        {
            Attach(childNode);
        }

        public Node(List<Node> childNodes)
        {
            foreach (Node child in childNodes)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node._parentNode = this;
            _childNodes.Add(node);
        }

        public virtual NodeState Evaluate() 
        {
            return NodeState.ERROR;
        }
    }

    public enum NodeState
    {
        SUCCESS,
        FAILURE,
        RUNNING,
        ERROR
    }
}


