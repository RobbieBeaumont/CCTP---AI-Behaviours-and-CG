using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.1f;
    public float CameraDistance = 1.0f;

    public Vector3 offset;

    private void Start()
    {
        transform.position = target.position + offset * CameraDistance;
    }

    //Re centers the camera smoothly when the player moves. 
    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset * CameraDistance;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, smoothedPosition.z);
        transform.LookAt(target);
    }
}
