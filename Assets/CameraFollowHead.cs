using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHead : MonoBehaviour
{
    public Transform target; // The transform of the head bone

    public Vector3 offsetPosition; // Offset from the head bone position
    public Vector3 offsetRotation; // Offset from the head bone rotation

    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position and rotation of the camera
            Vector3 desiredPosition = target.position + offsetPosition;
            Quaternion desiredRotation = target.rotation * Quaternion.Euler(offsetRotation);

            // Smoothly interpolate between the current position/rotation and the desired position/rotation
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);
        }
    }
}
