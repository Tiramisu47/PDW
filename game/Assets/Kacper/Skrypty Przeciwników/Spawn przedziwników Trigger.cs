using UnityEngine;

public class SpawnEnemyOnTrigger : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab wroga do spawnienia
    public Transform spawnPoint;  // Punkt, w którym wróg zostanie spawniony

    private bool hasSpawned = false; // Zapobiega wielokrotnemu spawnieniu

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzenie, czy gracz wchodzi w trigger
        if (other.CompareTag("Player") && !hasSpawned)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            hasSpawned = true; // Uniemo¿liwia ponowne spawnienie
        }
        else
        {
            Debug.LogWarning("Enemy prefab or spawn point is not assigned!");
        }
    }

    // Rysowanie sfery w edytorze dla lepszego podgl¹du triggera
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
