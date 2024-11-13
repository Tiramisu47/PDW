using UnityEngine;
using UnityEngine.AI;

public class SneakyStalker : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, player.forward);

        if (angle > 90)  // Gracz nie patrzy na przeciwnika
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();  // Zatrzymaj przeciwnika, gdy gracz patrzy
        }
    }
}
