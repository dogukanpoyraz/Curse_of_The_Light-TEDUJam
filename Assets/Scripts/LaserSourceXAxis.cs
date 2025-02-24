using UnityEngine;

public class LaserSourceXAxis : MonoBehaviour
{
    public Transform sourceTransform;
    public LaserBeam laserBeam;
    public AudioSource laserSound;

    public float speed = 2f;        // Movement speed
    public float switchTime = 10f;  // Time in seconds before switching direction

    private float timer = 0f;
    private bool movingForward = true; // Track movement direction

    private void Start()
    {
        // Play once at the start (it will loop if the AudioSource is set to loop)
        if (laserSound != null && !laserSound.isPlaying)
        {
            laserSound.Play();
        }

    }

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Switch direction every 'switchTime' seconds
        if (timer >= switchTime)
        {
            movingForward = !movingForward; // Toggle direction
            timer = 0f; // Reset timer
        }

        // Move the object along the Z-axis
        transform.position += (movingForward ? Vector3.forward : Vector3.back) * speed * Time.deltaTime;

        // Update laser beam position and direction
        Vector3 startPosition = sourceTransform.position;
        Vector3 direction = sourceTransform.forward;
        laserBeam.Propagate(startPosition, direction);
    }
}
