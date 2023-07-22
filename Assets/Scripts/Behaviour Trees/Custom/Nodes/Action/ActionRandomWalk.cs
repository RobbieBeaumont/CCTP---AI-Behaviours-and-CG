using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Performs a random walk (LEGACY)


public class ActionRandomWalk : Node
{
    private Transform _transform;
    private EnemyData _enemyData;
    private NavMeshAgent _navMeshAgent;

    private float _randomWalkRange;
    private Vector3 _destinationPos;

    private float _waitTime = 1.0f;
    private float _waitCount = 0.0f;
    private bool _waiting = true;

    public ActionRandomWalk(Transform transform, NavMeshAgent navMeshAgent)
    {
        _transform = transform;
        _enemyData = transform.GetComponent<EnemyData>();
        _randomWalkRange = _enemyData.randomWalkRange;
        _navMeshAgent = navMeshAgent;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCount += Time.deltaTime;
            if (_waitCount >= _waitTime)
            {
                _destinationPos = GenerateDestination();
                _waiting = false;
            }
        }
        else
        {
            if (Vector3.Distance(_transform.position, _destinationPos) < 0.01f)
            {
                Debug.Log("Position Reached");
                _transform.position = _destinationPos;
                _waitCount = 0.0f;
                _waiting = true;
            }
            else
            {
                _navMeshAgent.destination = _destinationPos;
            }
        }

        _nodeState = NodeState.RUNNING;
        return _nodeState;
    }

    private Vector3 GenerateDestination()
    {
        Vector2 randomDirectionNormalized = Random.insideUnitCircle.normalized * _randomWalkRange;
        Vector3 randomDirection = new(randomDirectionNormalized.x, _transform.position.y, randomDirectionNormalized.y);
        randomDirection += _transform.position;
        Debug.Log("Random Direction: " + randomDirection);
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _randomWalkRange, NavMesh.AllAreas);
        Vector3 finalPosition = new (hit.position.x, _transform.position.y, hit.position.z);
        Debug.Log("Generated Destination: " + finalPosition);
        return finalPosition;
    }
}
