using UnityEngine;

public class PlaneTrigger : MonoBehaviour
{
    public DoorInteraction door; // Odniesienie do skryptu DoorInteraction
    public bool planeActivated = false; // Flaga wskazuj�ca, czy dany Plane jest aktywowany
    public GameObject plane; // Zwi�zane z obiektem Plane (mo�e by� potrzebne w przysz�o�ci)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ustawiamy plane jako aktywowany, gdy gracz wchodzi w trigger
            if (!planeActivated)
            {
                planeActivated = true;
                Debug.Log($"Plane {gameObject.name} aktywowany.");
                door.CheckPuzzleSolved(); // Sprawdzamy, czy oba triggery zosta�y aktywowane
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Nie resetujemy stanu planeActivated po wyj�ciu gracza
            Debug.Log($"Plane {gameObject.name} nie dezaktywowany.");
            door.CheckPuzzleSolved(); // Sprawdzamy ponownie stan obu trigger�w
        }
    }
}
