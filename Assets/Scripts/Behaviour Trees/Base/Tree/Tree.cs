using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Mina Pecheux // Modified by Robbie Beaumont
// Base Tree class.

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        protected bool _isWaiting = true;
        protected Transform _target;
        private Node _rootNode = null;
        public Transform GetCurrentTarget() => _target;
        public void SetCurrentTarget(Transform target) => _target = target;
        public bool GetIsWaiting() => _isWaiting;
        public void SetIsWaiting(bool isWaiting) => _isWaiting = isWaiting;

        private void Start()
        {
            _rootNode = new Root(SetupTree());
        }

        void Update()
        {
            if (_rootNode != null)
            {
                _rootNode.Evaluate();
            }
        }
        protected abstract Node SetupTree();
    }
}
