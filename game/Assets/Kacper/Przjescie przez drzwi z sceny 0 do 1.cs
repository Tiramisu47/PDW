using UnityEngine;
using UnityEngine.SceneManagement; // Umo�liwia �adowanie poziom�w

public class DoorInteraction : MonoBehaviour
{
    public string nextSceneName; // Nazwa sceny, kt�r� chcemy za�adowa�
    private bool isPlayerInRange = false; // Flaga okre�laj�ca, czy gracz jest w zasi�gu drzwi
    private bool puzzleSolved = false; // Flaga okre�laj�ca, czy gracz rozwi�za� zagadk� z labiryntem

    void Update()
    {
        // Sprawdza, czy gracz jest w zasi�gu drzwi i czy nacisn�� klawisz "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (puzzleSolved)
                LoadNextLevel();
            else
            {
                Debug.Log("Drzwi s� zablokowane. Najpierw rozwi�� zagadk� z labiryntem!");
            }
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

    // Funkcja do odblokowania drzwi przez skrypt PlaneTrigger
    public void UnlockDoor()
    {
        puzzleSolved = true;
        Debug.Log("Zagadka rozwi�zana! Drzwi zosta�y odblokowane.");
    }
}
