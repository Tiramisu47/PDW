using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public string nextSceneName; // Nazwa sceny, któr¹ chcemy za³adowaæ
    private bool isPlayerInRange = false; // Flaga okreœlaj¹ca, czy gracz jest w zasiêgu drzwi
    public PlaneTrigger planeTrigger1; // Odniesienie do pierwszego PlaneTrigger
    public PlaneTrigger planeTrigger2; // Odniesienie do drugiego PlaneTrigger
    private bool puzzleSolved = false; // Flaga informuj¹ca, czy oba triggery zosta³y aktywowane

    void Update()
    {
        // Sprawdzamy, czy gracz jest w zasiêgu drzwi i oba triggery zosta³y aktywowane
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && puzzleSolved)
        {
            LoadNextLevel();
        }
        else if (isPlayerInRange && !puzzleSolved)
        {
            Debug.Log("Drzwi s¹ zablokowane. Najpierw rozwi¹¿ zagadkê!");
        }
    }

    // Funkcja do ³adowania nastêpnej sceny
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
            Debug.Log("Mo¿esz wejœæ przez drzwi. Naciœnij 'E'.");
        }
    }

    // Funkcja uruchamiana, gdy gracz opuszcza collider drzwi
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Wyszed³eœ z obszaru interakcji drzwi.");
        }
    }

    // Funkcja, która aktualizuje stan puzzle, gdy oba Plane s¹ aktywowane
    public void CheckPuzzleSolved()
    {
        if (planeTrigger1 != null && planeTrigger2 != null)
        {
            // Oba triggery musz¹ byæ aktywowane, aby puzzle zosta³o rozwi¹zane
            puzzleSolved = planeTrigger1.planeActivated && planeTrigger2.planeActivated;
        }
    }
}
