using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Checks the state of all the enemies in the current scene. 

public class ConditionCheckAllEnemiesState : Node
{
    private List<EnemyBT> _enemies;
    private EnemyState _stateToCheck;
    private int _matchedEnemyStateCount = 0;
    public ConditionCheckAllEnemiesState(List<EnemyBT> enemies, EnemyState stateToCheck)
    {
        _enemies = enemies;
        _stateToCheck = stateToCheck;
    }

    public override NodeState Evaluate()
    {
        foreach (EnemyBT enemyBT in _enemies)
        {
            if (_stateToCheck == enemyBT.GetEnemyState())
            {
                _matchedEnemyStateCount++;
            }
        }
        
        if (_matchedEnemyStateCount == _enemies.Count || _matchedEnemyStateCount == 0)
        {
            _nodeState = NodeState.FAILURE;
        }
        else
        {
            _nodeState = NodeState.SUCCESS;
        }
        _matchedEnemyStateCount = 0;
        return _nodeState;
    }
}
