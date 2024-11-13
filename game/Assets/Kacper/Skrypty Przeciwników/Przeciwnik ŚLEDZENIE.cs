using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{
    public Transform player; // Przeciwnik będzie śledził tego gracza
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Śledzenie pozycji gracza
        }
    }
}
