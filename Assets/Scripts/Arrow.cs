using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatAmplitude = 0.5f; // Yukar� a�a�� hareket mesafesi
    public float floatSpeed = 2f; // Yukar� a�a�� hareket h�z�

    [Header("Rotation Settings")]
    public float rotationSpeed = 50f; // Kendi etraf�nda d�n�� h�z�

    private Vector3 startPosition;

    void Start()
    {
        // Ba�lang�� pozisyonunu kaydet
        startPosition = transform.position;
    }

    void Update()
    {
        // Yukar� a�a�� hareket
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Kendi etraf�nda d�nme
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
