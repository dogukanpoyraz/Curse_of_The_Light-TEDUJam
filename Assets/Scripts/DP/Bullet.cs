using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Bir �eye �arpt���nda mermiyi yok et
    }
}
