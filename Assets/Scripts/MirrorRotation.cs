using UnityEngine;

public class MirrorRotation : MonoBehaviour
{
    public float rotationSpeed = 25f; // Speed of rotation (degrees per second)

    void Update()
    {
        // Rotate the object around its own Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
