using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Compares the state of enemy against a desired state on a tree.

public class ConditionCompareEnemyStates : Node
{
    private Transform _transform;
    private EnemyState _desiredState;

    public ConditionCompareEnemyStates(Transform transform, EnemyState desiredState)
    {
        _transform = transform;
        _desiredState = desiredState;
    }

    public override NodeState Evaluate()
    {

        if (_desiredState == _transform.GetComponent<EnemyBT>().GetEnemyState())
        {
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }
        return _nodeState;
    }
}
