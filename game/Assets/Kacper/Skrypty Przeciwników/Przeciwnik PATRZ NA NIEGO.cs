using UnityEngine;
using UnityEngine.AI;

public class SneakyStalker : MonoBehaviour
{
    public float viewAngle = 90f;        // K�t widzenia gracza
    public float detectionRange = 10f;   // Maksymalna odleg�o�� wykrycia przeciwnika
    public float moveSpeed = 3.5f;      // Pr�dko�� poruszania si� przeciwnika

    private Transform player;            // Transform gracza
    private Transform playerCamera;      // Transform kamery gracza
    private NavMeshAgent agent;          // Agent do poruszania przeciwnikiem

    void Start()
    {
        // Znajdowanie gracza automatycznie w scenie
        player = GameObject.FindWithTag("Player").transform;

        // Znajdowanie g��wnej kamery w scenie
        playerCamera = Camera.main.transform;

        // Pobranie komponentu NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Sprawdzenie, czy znaleziono gracza i kamer�
        if (player == null)
        {
            Debug.LogError("Nie znaleziono gracza w scenie!");
        }
        if (playerCamera == null)
        {
            Debug.LogError("Nie znaleziono kamery w scenie!");
        }

        // Ustawienie pr�dko�ci poruszania si� na warto�� moveSpeed
        agent.speed = moveSpeed;
    }

    void Update()
    {
        if (player != null && playerCamera != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            // Sprawdzenie, czy przeciwnik znajduje si� w zasi�gu wykrycia
            if (distanceToPlayer <= detectionRange)
            {
                // Normalizowanie kierunku do gracza
                directionToPlayer.Normalize();

                // Sprawdzamy, czy gracz patrzy w kierunku przeciwnika
                float angle = Vector3.Angle(playerCamera.forward, -directionToPlayer);

                if (angle > viewAngle / 2f)  // Gracz nie patrzy na przeciwnika
                {
                    agent.SetDestination(player.position);  // Przeciwnik pod��a za graczem
                }
                else
                {
                    agent.ResetPath();  // Przeciwnik zatrzymuje si�, gdy gracz patrzy
                }
            }
            else
            {
                agent.ResetPath();  // Zatrzymaj przeciwnika, je�li jest poza zasi�giem
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Rysowanie zasi�gu detekcji w edytorze
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Rysowanie k�ta widzenia
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
