using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Transform that travels to a target.

public class ActionTravelToTarget : Node
{
    private Transform _transform;
    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private float _minimumDistanceToTarget;
    
    public ActionTravelToTarget(Transform transform, NavMeshAgent navMeshAgent, float minimumDistanceToTarget)
    {
        _transform = transform;
        _navMeshAgent = navMeshAgent;
        _minimumDistanceToTarget = minimumDistanceToTarget;
    }

    public override NodeState Evaluate()
    {
        _target = _transform.GetComponent<EnemyBT>().GetCurrentTarget();
        _target.position = new Vector3(_target.position.x, _transform.position.y, _target.position.z);
        if (Vector3.Distance(_transform.position, _target.position) < _minimumDistanceToTarget)
        {
            _nodeState = NodeState.SUCCESS;
            _transform.GetComponent<EnemyBT>().SetCurrentTarget(null);
            return _nodeState;
        }
        else
        {
            _navMeshAgent.destination = _target.position;
            _nodeState = NodeState.RUNNING;
        }
        return _nodeState;
    }
}
