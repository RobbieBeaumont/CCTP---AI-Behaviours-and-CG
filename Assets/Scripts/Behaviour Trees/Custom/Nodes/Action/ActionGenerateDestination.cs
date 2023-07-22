using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Generates a valid destination on a navmesh.

public class ActionGenerateDestination : Node
{
    private Transform _transform;
    private Transform _defaultTarget;
    private float _walkRange;

    public ActionGenerateDestination(Transform transform, Transform defaultTarget, float walkRange)
    {
        _transform = transform;
        _defaultTarget = defaultTarget;
        _walkRange = walkRange;
    }

    public override NodeState Evaluate()
    {
        if (_transform.GetComponent<EnemyBT>().GetCurrentTarget() == null && !_transform.GetComponent<EnemyBT>().GetIsWaiting())
        {
            _transform.GetComponent<EnemyBT>().SetCurrentTarget(GenerateDestination());
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }
        return _nodeState;
    }

    private Transform GenerateDestination()
    {
        Vector2 randomDirectionNormalized = Random.insideUnitCircle.normalized * _walkRange;
        Vector3 randomDirection = new(randomDirectionNormalized.x, _transform.position.y, randomDirectionNormalized.y);
        randomDirection += _transform.position;
        Debug.Log("Random Direction: " + randomDirection);

        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _walkRange, NavMesh.AllAreas);
        Vector3 finalPosition = new(hit.position.x, _transform.position.y, hit.position.z);
        Debug.Log("Generated Destination: " + finalPosition);

        _defaultTarget.position = finalPosition;
        return _defaultTarget;
    }
}
