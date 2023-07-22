using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont

public class CameraTarget : MonoBehaviour
{

    public Transform player;

    public float smoothSpeed = 0.1f;

    private float _startZ = 0.0f;

    void Start()
    {
        //_startZ = player.position.z;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, smoothedPosition.z);
    }
}
