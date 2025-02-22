using UnityEngine;

public class Level2Rotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private Transform centerPoint; // Dönüþ merkezi
    [SerializeField] private float rotationSpeed = 30f; // Derece/saniye
    [SerializeField] private float orbitRadius = 2f; // Merkezden uzaklýk
    [SerializeField] private bool clockwise = true; // Dönüþ yönü

    private Vector3 _axis = Vector3.forward; // Z ekseni

    void Start()
    {
        // Baþlangýç pozisyonunu ayarla
        transform.position = centerPoint.position + new Vector3(orbitRadius, 0, 0);
    }

    void Update()
    {
        // Dönüþ yönünü belirle
        float direction = clockwise ? -1f : 1f;

        // Merkez etrafýnda döndür
        transform.RotateAround(
            centerPoint.position,
            _axis,
            rotationSpeed * direction * Time.deltaTime
        );
    }
}