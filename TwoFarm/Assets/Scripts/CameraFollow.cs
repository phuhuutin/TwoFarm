using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;    // Reference to the character's transform
    public float SmoothSpeed = 0.125f; // Speed at which the camera catches up to the target
    public Vector3 Offset;      // Offset between the camera and the character

    public float ZoomSpeed = 5f; // Speed of zooming in and out
    public float MinZoom = 5f; // Minimum orthographic size for zooming in
    public float MaxZoom = 10f; // Maximum orthographic size for zooming out


    private Camera cam;         // Reference to the Camera component

    void Start()
    {
        // Get the Camera component attached to this GameObject
        cam = GetComponent<Camera>();
    }
    void LateUpdate()
    {


        if (Target != null)
        {
            // Define the target position with the offset
            Vector3 desiredPosition = Target.position + Offset;

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
            transform.position = smoothedPosition;

            // Handle zooming in and out
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                float newSize = cam.orthographicSize - scrollInput * ZoomSpeed;

                if (newSize < MinZoom)
                {
                    newSize = MinZoom;
                }
                else if (newSize > MaxZoom)
                {
                    newSize = MaxZoom;
                }

                cam.orthographicSize = newSize;
            }
        }


    }
}
