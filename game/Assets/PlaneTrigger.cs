using UnityEngine;

public class PlaneTrigger : MonoBehaviour
{
    public DoorInteraction door; // Przypisz obiekt drzwi w inspectorze
    private int requiredCubes = 3;
    private int placedCubes = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            placedCubes++;
            Debug.Log("Umieszczono cube: " + placedCubes + "/" + requiredCubes);

            if (placedCubes >= requiredCubes)
            {
                door.UnlockDoor();
            }
        }
    }
}
