using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;    // Mermi h�z�
    public int damage = 20;      // Hasar miktar�
    public Rigidbody rb;

    void Start()
    {
        rb.velocity = transform.forward * speed; // Mermiyi ileriye do�ru hareket ettir
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))  // E�er d��mana �arparsa
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
        Destroy(gameObject); // Bir �eye �arpt���nda mermiyi yok et
    }
}
