using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Changes a group of enemies states.

public class ActionChangeEnemyState : Node
{
    private List<EnemyBT> _enemies;
    private EnemyState _stateToChangeTo;
    public ActionChangeEnemyState(List<EnemyBT> enemies, EnemyState stateToChangeTo)
    {
        _enemies = enemies;
        _stateToChangeTo = stateToChangeTo;
    }

    public override NodeState Evaluate()
    {
        foreach (EnemyBT enemyBT in _enemies)
        {
            enemyBT.SetEnemyState(_stateToChangeTo);
            if (enemyBT.GetEnemyState() != _stateToChangeTo)
            {
                _nodeState = NodeState.FAILURE;
                return _nodeState;
            }
        }
        _nodeState = NodeState.SUCCESS;
        return _nodeState;    
    }
}
