using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Uses a random chance to change the state.

public class ActionChanceToChangeState : Node
{
    private Transform _transform;
    private EnemyState _stateToChangeTo;
    private float _percentageChance;

    public ActionChanceToChangeState(Transform transform, EnemyState stateToChangeTo, float percentageChance)
    {
        _transform = transform;
        _stateToChangeTo = stateToChangeTo;
        _percentageChance = percentageChance / 100;
    }

    public override NodeState Evaluate()
    {
        float randomNumber = Random.value;
        if (randomNumber <= _percentageChance)
        {
            _transform.GetComponent<EnemyBT>().SetEnemyState(_stateToChangeTo);
            _transform.GetComponent<EnemyBT>().SetCurrentTarget(null);
            _transform.GetComponent<EnemyBT>().SetIsWaiting(false);
            _nodeState = NodeState.SUCCESS;
        }
        else 
        {
            _nodeState = NodeState.FAILURE;
        }
        return _nodeState;
    }
}
