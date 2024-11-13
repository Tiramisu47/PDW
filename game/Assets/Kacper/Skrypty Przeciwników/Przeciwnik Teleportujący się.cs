using UnityEngine;

public class TeleportingEnemy : MonoBehaviour
{
    public Transform player;
    public float teleportDistance = 5f;
    public float teleportCooldown = 3f;

    void Start()
    {
        InvokeRepeating("TeleportToPlayer", teleportCooldown, teleportCooldown);
    }

    void TeleportToPlayer()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-teleportDistance, teleportDistance),
            0,
            Random.Range(-teleportDistance, teleportDistance)
        );

        transform.position = player.position + randomOffset;
    }
}
