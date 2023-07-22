using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Checks line of sight to waypoints.

public class ConditionLineOfSightToWaypoint : Node
{
    private Transform _transform;
    private Transform _player;
    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private HidingSpots _hidingSpots;
    private List<Waypoints> _waypoints = new List<Waypoints>();
    private bool _waypointAvailable = false;
    private float _shortestDistanceToWaypoint = float.MaxValue;

    public ConditionLineOfSightToWaypoint(Transform transform, GameObject player, NavMeshAgent navMeshAgent, HidingSpots hidingSpots)
    {
        _player = player.transform;
        _transform = transform;
        _navMeshAgent = navMeshAgent;
        _hidingSpots = hidingSpots;
        List<Waypoints> waypoints = new List<Waypoints>(hidingSpots.GetHidingSpotsList());
        foreach (Waypoints waypoint in waypoints)
        {
            AttachWaypoint(_waypoints, waypoint);
        }
    }

    public override NodeState Evaluate()
    {
        foreach (Waypoints waypoint in _waypoints)
        {
            Vector3 waypointPosition = waypoint.GetTransform().position;
            if (Physics.Linecast(_player.position, waypointPosition))
            {
                _waypointAvailable = true;
                waypoint.SetDistanceToWaypoint(CalculatePathDistance(waypointPosition));
                
                if (waypoint.GetDistanceToWaypoint() <= _shortestDistanceToWaypoint)
                {
                    _shortestDistanceToWaypoint = waypoint.GetDistanceToWaypoint();
                    _target = waypoint.GetTransform();
                }
            }
        }

        if (_waypointAvailable)
        {
            _transform.GetComponent<EnemyBT>().SetCurrentTarget(_target);
            _waypointAvailable = false;
            _shortestDistanceToWaypoint = float.MaxValue;
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }
        return _nodeState;
    }

    // Calculates the distance along the navmesh from the transform to the waypoint. 
    private float CalculatePathDistance(Vector3 waypointPosition)
    {
        NavMeshPath path = new NavMeshPath();
        float distance = 0;
        if (_navMeshAgent.CalculatePath(waypointPosition, path))
        {
            distance = Vector3.Distance(_transform.position, path.corners[0]);
            for (int i = 1; i < path.corners.Length; i++)
            {
                distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }
        return distance;
    }

    private void AttachWaypoint(List<Waypoints> list, Waypoints waypoint)
    {
        list.Add(waypoint);
    }

}
