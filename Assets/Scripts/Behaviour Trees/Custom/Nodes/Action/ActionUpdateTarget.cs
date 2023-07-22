using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Updates the current target of an AI agent.

public class ActionUpdateTarget : Node
{
    private Transform _transform;
    private Transform _target;
    public ActionUpdateTarget(Transform transform, Transform target)
    {
        _transform = transform;
        _target = target;
    }

    public override NodeState Evaluate()
    {
        if (_target != _transform.GetComponent<EnemyBT>().GetCurrentTarget())
        {
            _transform.GetComponent<EnemyBT>().SetCurrentTarget(_target);
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }
        return _nodeState;
    }
}
