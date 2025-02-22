using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public Animator animator;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public bool IsAttacking = false;
    public bool IsDeading = false;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player prefab not found in the scene!");
        }

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the enemy!");
        }
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange) AttackPlayer();

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        if (!IsAttacking && !IsDeading) // Ölüm veya saldırı durumunda tekrar saldırmaması için
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            IsAttacking = true;
            animator.SetBool("IsAttacking", true); // Saldırı animasyonunu tetikle
            Debug.Log("Saldırı animasyonu başladı!");

            // Oyuncuya hasar verme
            float damageAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke(nameof(DealDamageToPlayer), damageAnimationLength); // Saldırı animasyonunun bitiminden sonra hasar ver
            Invoke(nameof(ResetAttack), 1.5f); // 1.5 saniye sonra saldırıyı tekrar yapabilir
        }
    }

    private void DealDamageToPlayer()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.health -= 10; // Hasar miktarını buradan ayarlayabilirsiniz
            Debug.Log("Oyuncuya hasar verildi: " + playerMovement.health);
        }
    }

    private void ResetAttack()
    {
        IsAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        if (IsDeading) return; // Eğer zaten ölüyorsa tekrar işlem yapma.

        health -= damage;
        Debug.Log(health);

        if (health <= 0)
        {
            IsDeading = true;
            IsAttacking = false; // Eğer saldırıyorsa iptal et.

            // Tüm animasyonları sıfırla ve ölüm animasyonunu başlat.
            animator.SetBool("IsAttacking", false);
            animator.SetFloat("Speed", 0);
            animator.SetTrigger("IsDeading 0");

            // Hareketi durdur ama NavMeshAgent'ı devre dışı bırakma.
            agent.isStopped = true;

            // Çarpışmaları iptal et ki karakter yere düşsün veya fiziksel etkiler uygulanabilsin.
            //GetComponent<Collider>().enabled = false;

            // Ölüm animasyonunun süresini al ve o süre kadar Destroy işlemini beklet.
            float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke(nameof(DestroyEnemy), deathAnimationLength);
        }
    }




    private void DestroyEnemy()
    {

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}