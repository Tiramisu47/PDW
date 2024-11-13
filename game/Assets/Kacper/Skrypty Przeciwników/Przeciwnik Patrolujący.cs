using UnityEngine;
using UnityEngine.AI;

enum EnemyState { Patrol, Chase }

public class PatrolAndChaseEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private EnemyState state = EnemyState.Patrol;
    public float chaseDistance = 10f;
    public float stopChaseDistance = 15f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrolPoints[0].position);
    }

    void Update()
    {
        if (state == EnemyState.Patrol)
        {
            Patrol();
            CheckForChase();
        }
        else if (state == EnemyState.Chase)
        {
            Chase();
            CheckStopChase();
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void CheckForChase()
    {
        if (Vector3.Distance(player.position, transform.position) < chaseDistance)
        {
            state = EnemyState.Chase;
        }
    }

    void CheckStopChase()
    {
        if (Vector3.Distance(player.position, transform.position) > stopChaseDistance)
        {
            state = EnemyState.Patrol;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }
}
