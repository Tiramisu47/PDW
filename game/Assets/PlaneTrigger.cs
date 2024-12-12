using UnityEngine;

public class PlaneTrigger : MonoBehaviour
{
    public DoorInteraction door; // Odniesienie do skryptu DoorInteraction
    public bool planeActivated = false; // Flaga wskazuj¹ca, czy dany Plane jest aktywowany
    public GameObject plane; // Zwi¹zane z obiektem Plane (mo¿e byæ potrzebne w przysz³oœci)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ustawiamy plane jako aktywowany, gdy gracz wchodzi w trigger
            if (!planeActivated)
            {
                planeActivated = true;
                Debug.Log($"Plane {gameObject.name} aktywowany.");
                door.CheckPuzzleSolved(); // Sprawdzamy, czy oba triggery zosta³y aktywowane
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Nie resetujemy stanu planeActivated po wyjœciu gracza
            Debug.Log($"Plane {gameObject.name} nie dezaktywowany.");
            door.CheckPuzzleSolved(); // Sprawdzamy ponownie stan obu triggerów
        }
    }
}
