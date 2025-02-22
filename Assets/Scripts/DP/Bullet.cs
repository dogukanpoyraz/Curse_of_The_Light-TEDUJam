using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Bir þeye çarptýðýnda mermiyi yok et
    }
}
