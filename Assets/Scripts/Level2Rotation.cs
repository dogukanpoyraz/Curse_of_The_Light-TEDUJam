using UnityEngine;

public class Level2Rotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private Transform centerPoint; // D�n�� merkezi
    [SerializeField] private float rotationSpeed = 30f; // Derece/saniye
    [SerializeField] private float orbitRadius = 2f; // Merkezden uzakl�k
    [SerializeField] private bool clockwise = true; // D�n�� y�n�

    private Vector3 _axis = Vector3.forward; // Z ekseni

    void Start()
    {
        // Ba�lang�� pozisyonunu ayarla
        transform.position = centerPoint.position + new Vector3(orbitRadius, 0, 0);
    }

    void Update()
    {
        // D�n�� y�n�n� belirle
        float direction = clockwise ? -1f : 1f;

        // Merkez etraf�nda d�nd�r
        transform.RotateAround(
            centerPoint.position,
            _axis,
            rotationSpeed * direction * Time.deltaTime
        );
    }
}