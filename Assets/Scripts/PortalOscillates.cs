using UnityEngine;

public class PortalOscillates : MonoBehaviour
{
    public Transform portalTransform;

    public float speed = 2f; 
    public float switchTime = 10f; 

    private float timer = 0f;
    private bool movingForward = true; 

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchTime)
        {
            movingForward = !movingForward; 
            timer = 0f; 
        }

        transform.position += (movingForward ? Vector3.forward : Vector3.back) * speed * Time.deltaTime;

        Vector3 startPosition = portalTransform.position;
        Vector3 direction = portalTransform.forward;
    }
}
