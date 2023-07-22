using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Checks the current player state against a desired state on a tree.

public class ConditionCheckPlayerState : Node
{
    private PlayerData _playerData;
    private PlayerState _desiredState;
    public ConditionCheckPlayerState(PlayerData playerData, PlayerState desiredState)
    {
        _playerData = playerData;
        _desiredState = desiredState;
    }

    public override NodeState Evaluate()
    {
        if (_playerData.GetPlayerState() == _desiredState)
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
