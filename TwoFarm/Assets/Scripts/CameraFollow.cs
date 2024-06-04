using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // Reference to the character's transform
    public float smoothSpeed = 0.125f; // Speed at which the camera catches up to the target
    public Vector3 offset;      // Offset between the camera and the character

    void LateUpdate()
    {
        if (target != null)
        {
            // Define the target position with the offset
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // Maintain the camera's original Z position

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera position
            transform.position = smoothedPosition;
        }
    }
}
