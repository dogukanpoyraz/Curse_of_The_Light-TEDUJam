using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatAmplitude = 0.5f; // Yukarý aþaðý hareket mesafesi
    public float floatSpeed = 2f; // Yukarý aþaðý hareket hýzý

    [Header("Rotation Settings")]
    public float rotationSpeed = 50f; // Kendi etrafýnda dönüþ hýzý

    private Vector3 startPosition;

    void Start()
    {
        // Baþlangýç pozisyonunu kaydet
        startPosition = transform.position;
    }

    void Update()
    {
        // Yukarý aþaðý hareket
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Kendi etrafýnda dönme
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
