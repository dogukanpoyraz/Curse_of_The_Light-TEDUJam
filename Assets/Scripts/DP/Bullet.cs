using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;    // Mermi hýzý
    public int damage = 20;      // Hasar miktarý
    public Rigidbody rb;

    void Start()
    {
        rb.velocity = transform.forward * speed; // Mermiyi ileriye doðru hareket ettir
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))  // Eðer düþmana çarparsa
        {
            EnemyAiTutorial enemy = other.GetComponent<EnemyAiTutorial>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Hasar ver
            }
            Destroy(gameObject); // Mermiyi yok et
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Bir þeye çarptýðýnda mermiyi yok et
    }
}
