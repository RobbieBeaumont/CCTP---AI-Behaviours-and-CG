using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Checks the line of sight between an object and the player.

public class ConditionLineOfSightToPlayer : Node
{
    private Transform _transform;
    private Transform _player;

    public ConditionLineOfSightToPlayer(Transform transform, Transform player)
    {
        _transform = transform;
        _player = player;
    }

    public override NodeState Evaluate()
    {
        if (!Physics.Linecast(_transform.position, _player.position))
        {
            _transform.GetComponent<EnemyBT>().SetEnemyState(EnemyState.ATTACKING);
            _transform.GetComponent<EnemyBT>().SetCurrentTarget(null);
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }
        return _nodeState;
    }
}
