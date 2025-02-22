using UnityEngine;

public class LaserSource : MonoBehaviour
{
    public Transform sourceTransform;
    public LaserBeam laserBeam;

    public float speed = 5f; // Speed of movement
    public float minZ = -5f; // Minimum Z position
    public float maxZ = 5f;  // Maximum Z position

    private bool movingForward = true; // Track movement direction

    private void Update()
    {
        // Move the object along the Z-axis
        if (movingForward)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            if (transform.position.z >= maxZ)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
            if (transform.position.z <= minZ)
            {
                movingForward = true;
            }
        }

        // Update laser beam position and direction
        Vector3 startPosition = sourceTransform.position;
        Vector3 direction = sourceTransform.forward;
        laserBeam.Propagate(startPosition, direction);
    }
}
