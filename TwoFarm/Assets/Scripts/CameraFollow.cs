using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;    // Reference to the character's transform
    public float SmoothSpeed = 0.125f; // Speed at which the camera catches up to the target
    public Vector3 Offset;      // Offset between the camera and the character

    void LateUpdate()
    {
        if (Target != null)
        {
            // Define the target position with the offset
            Vector3 desiredPosition = Target.position + Offset;
            desiredPosition.z = transform.position.z; // Maintain the camera's original Z position

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);

            // Update the camera position
            transform.position = smoothedPosition;
        }
    }
}
