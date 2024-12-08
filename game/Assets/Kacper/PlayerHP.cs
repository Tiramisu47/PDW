using Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10; // Maksymalne zdrowie gracza
    private int currentHealth; // Obecne zdrowie gracza

    private void Start()
    {
        currentHealth = maxHealth; // Ustawiamy pocz�tkowe zdrowie
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Gracz zgin��!"); // Tutaj mo�esz doda� system �mierci gracza
        }
        else
        {
            Debug.Log($"Gracz otrzyma� {damage} obra�e�. Aktualne zdrowie: {currentHealth}");
        }

        // Uruchom efekt na kamerze (przyjmiemy, �e ten skrypt jest dodany do gracza)
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake != null)
        {
            cameraShake.ShakeCamera();
        }
    }
}
