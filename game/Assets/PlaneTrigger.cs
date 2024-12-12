using UnityEngine;

public class PlaneTrigger : MonoBehaviour
{
    public DoorInteraction door; // Odniesienie do skryptu DoorInteraction
    public GameObject plane1; // Pierwszy Plane
    public GameObject plane2; // Drugi Plane

    public bool puzzleSolved;
    private bool plane1Activated = false;
    private bool plane2Activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject == plane1)
            {
                plane1Activated = true;
                Debug.Log("Plane 1 aktywowany.");
            }
            else if (gameObject == plane2)
            {
                plane2Activated = true;
                Debug.Log("Plane 2 aktywowany.");
            }
        }
        // Sprawdzenie, czy oba Plane zosta³y aktywowane
        if (plane1Activated && plane2Activated)
        {
            puzzleSolved = true;
            Debug.LogWarning("Oba Plane aktywowane. Drzwi odblokowane!");
        }
    }
}
