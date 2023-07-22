using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont
// Base waypoint class which stores the distance between the enemies and the waypoint.

public class Waypoints
{
    private Transform _transform;
    private float _distanceToWaypoint;

    public Transform GetTransform() => _transform;
    public float GetDistanceToWaypoint() => _distanceToWaypoint;
    public void SetDistanceToWaypoint(float distanceToWaypoint) => _distanceToWaypoint = distanceToWaypoint;


    public Waypoints(Transform transform)
    {
        _transform = transform;
        _distanceToWaypoint = 0;
    }
}
