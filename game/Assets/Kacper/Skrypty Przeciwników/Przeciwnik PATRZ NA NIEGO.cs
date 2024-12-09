using UnityEngine;
using UnityEngine.AI;

public class SneakyStalker : MonoBehaviour
{
    public float viewAngle = 90f;            // K¹t widzenia gracza
    public float detectionRange = 10f;       // Maksymalna odleg³oœæ wykrycia gracza
    public float moveSpeed = 3.5f;           // Prêdkoœæ poruszania siê przeciwnika
    public float attackRange = 2f;           // Zasiêg ataku
    public float attackCooldown = 2f;       // Czas miêdzy atakami

    private Transform player;                // Transform gracza
    private Transform playerCamera;          // Transform kamery gracza
    private NavMeshAgent agent;              // Agent do poruszania przeciwnikiem
    private Animator animator;               // Animator przeciwnika
    private float lastAttackTime = 0f;       // Czas ostatniego ataku

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerCamera = Camera.main.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (player == null) Debug.LogError("Nie znaleziono gracza w scenie!");
        if (playerCamera == null) Debug.LogError("Nie znaleziono kamery w scenie!");

        agent.speed = moveSpeed;
    }

    void Update()
    {
        if (player != null && playerCamera != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer <= detectionRange)
            {
                directionToPlayer.Normalize();
                float angle = Vector3.Angle(playerCamera.forward, -directionToPlayer);

                if (angle > viewAngle / 2f)
                {
                    agent.SetDestination(player.position);
                    animator.SetBool("IsRunning", true); // Biegnie za graczem

                    if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
                    {
                        AttackPlayer();
                        lastAttackTime = Time.time;
                    }
                }
                else
                {
                    agent.ResetPath();
                    animator.SetBool("IsRunning", false); // Przestaje biec
                }
            }
            else
            {
                agent.ResetPath();
                animator.SetBool("IsRunning", false); // Przestaje biec
            }
        }
    }

    private void AttackPlayer()
    {
        agent.ResetPath();
        animator.SetBool("IsAttacking", true); // Uruchom animacjê ataku
        Invoke("EndAttack", 0.5f); // Zakoñcz animacjê ataku po 0.5 sekundy (czas trwania ataku)

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1); // Zadaj 1 punkt obra¿eñ graczowi
        }
    }

    private void EndAttack()
    {
        animator.SetBool("IsAttacking", false); // Zakoñcz animacjê ataku
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 forward = transform.forward;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-viewAngle / 2f, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(viewAngle / 2f, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftRayDirection * detectionRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * detectionRange);
    }
}
