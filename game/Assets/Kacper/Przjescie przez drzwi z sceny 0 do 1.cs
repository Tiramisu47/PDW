using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public string nextSceneName; // Nazwa sceny, kt�r� chcemy za�adowa�
    private bool isPlayerInRange = false; // Flaga okre�laj�ca, czy gracz jest w zasi�gu drzwi
    public PlaneTrigger planeTrigger1; // Odniesienie do pierwszego PlaneTrigger
    public PlaneTrigger planeTrigger2; // Odniesienie do drugiego PlaneTrigger
    private bool puzzleSolved = false; // Flaga informuj�ca, czy oba triggery zosta�y aktywowane

    void Update()
    {
        // Sprawdzamy, czy gracz jest w zasi�gu drzwi i oba triggery zosta�y aktywowane
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && puzzleSolved)
        {
            LoadNextLevel();
        }
        else if (isPlayerInRange && !puzzleSolved)
        {
            Debug.Log("Drzwi s� zablokowane. Najpierw rozwi�� zagadk�!");
        }
    }

    // Funkcja do �adowania nast�pnej sceny
    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    // Funkcja uruchamiana, gdy gracz wchodzi w collider drzwi
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Mo�esz wej�� przez drzwi. Naci�nij 'E'.");
        }
    }

    // Funkcja uruchamiana, gdy gracz opuszcza collider drzwi
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Wyszed�e� z obszaru interakcji drzwi.");
        }
    }

    // Funkcja, kt�ra aktualizuje stan puzzle, gdy oba Plane s� aktywowane
    public void CheckPuzzleSolved()
    {
        if (planeTrigger1 != null && planeTrigger2 != null)
        {
            // Oba triggery musz� by� aktywowane, aby puzzle zosta�o rozwi�zane
            puzzleSolved = planeTrigger1.planeActivated && planeTrigger2.planeActivated;
        }
    }
}
