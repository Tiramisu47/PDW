using UnityEngine;
using UnityEngine.SceneManagement; // Umo¿liwia ³adowanie poziomów

public class DoorInteraction : MonoBehaviour
{
    public string nextSceneName; // Nazwa sceny, któr¹ chcemy za³adowaæ
    private bool isPlayerInRange = false; // Flaga okreœlaj¹ca, czy gracz jest w zasiêgu drzwi
    private bool puzzleSolved = false; // Flaga okreœlaj¹ca, czy gracz rozwi¹za³ zagadkê z labiryntem

    void Update()
    {
        // Sprawdza, czy gracz jest w zasiêgu drzwi i czy nacisn¹³ klawisz "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (puzzleSolved)
                LoadNextLevel();
            else
            {
                Debug.Log("Drzwi s¹ zablokowane. Najpierw rozwi¹¿ zagadkê z labiryntem!");
            }
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

    // Funkcja do odblokowania drzwi przez skrypt PlaneTrigger
    public void UnlockDoor()
    {
        puzzleSolved = true;
        Debug.Log("Zagadka rozwi¹zana! Drzwi zosta³y odblokowane.");
    }
}
