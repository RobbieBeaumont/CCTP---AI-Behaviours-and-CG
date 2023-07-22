using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Attacks a given target.

public class ActionAttackTarget : Node
{
    private PlayerData _player;
    private EnemyData _enemy;
    private Transform _transform;
    private float _range;
    private static int _playerLayerMask = 1 << 7;
    public ActionAttackTarget(PlayerData playerData, Transform transform, float attackRange)
    {
        _player = playerData;
        _enemy = transform.GetComponent<EnemyData>();
        _transform = transform;
        _range = attackRange;
    }

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, _range, _playerLayerMask);

        if (colliders.Length > 0)
        {
            _player.TakeDamage(_enemy.GetAttackDamage());
        }

        _transform.GetComponent<EnemyBT>().SetCurrentTarget(null);
        _nodeState = NodeState.SUCCESS;

        return _nodeState;
    }
}
