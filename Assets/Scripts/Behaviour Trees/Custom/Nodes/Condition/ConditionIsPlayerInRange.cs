using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Checks if the player is in a range around a transform.

public class ConditionIsPlayerInRange : Node
{
    private static int _playerLayerMask = 1 << 7;

    private Transform _transform;
    private float _range;
    private EnemyState _updatedState;

    public ConditionIsPlayerInRange(Transform transform, float range)
    {
        _transform = transform;
        _range = range;
        _updatedState = EnemyState.NULL;
    }

    public ConditionIsPlayerInRange(Transform transform, float range, EnemyState updatedState)
    {
        _transform = transform;
        _range = range;
        _updatedState = updatedState;
    }

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, _range, _playerLayerMask);

        if (colliders.Length > 0)
        {
            if (_updatedState != EnemyState.NULL)
            {
                _transform.GetComponent<EnemyBT>().SetEnemyState(_updatedState);
                _transform.GetComponent<EnemyBT>().SetCurrentTarget(null);
            }
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }
        return _nodeState;
    }
}
