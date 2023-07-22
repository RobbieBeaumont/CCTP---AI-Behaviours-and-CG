using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

// Class Authored by Robbie Beaumont
// Waits for a given period of time in seconds.

public class ActionWaitInSeconds : Node
{
    private float _waitTime = 0f;
    private float _waitCounter = 0f;
    private BehaviourTree.Tree _tree;

    public ActionWaitInSeconds(float waitTime, BehaviourTree.Tree tree)
    {
        _waitTime = waitTime;
        _tree = tree;
    }

    public override NodeState Evaluate()
    {
        if (_tree.GetIsWaiting())
        {
            _waitCounter += Time.deltaTime;
            _nodeState = NodeState.RUNNING;
            if (_waitCounter >= _waitTime)
            {
                _tree.SetIsWaiting(false);
                _nodeState = NodeState.SUCCESS;
                return _nodeState;
            }
        }
        else
        {
            if (_tree.GetCurrentTarget() == null)
            {
                _tree.SetIsWaiting(true);
                _waitCounter = 0;
                _nodeState = NodeState.RUNNING;
            }
        }
        return _nodeState;
    }
}
