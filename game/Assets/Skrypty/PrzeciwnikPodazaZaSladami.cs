using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrzeciwnikPodazaZaSladami : MonoBehaviour
{
    public SladyGracza sladyGracza;    // Referencja do skryptu śladów gracza
    public float predkosc = 3.0f;      // Prędkość przeciwnika
    public float minimalnaOdleglosc = 0.1f; // Minimalna odległość do punktu ścieżki

    private int indeksAktualnegoSladu = 0;

    private void Update()
    {
        // Jeśli lista śladów jest pusta, nic nie robi
        if (sladyGracza.slady.Count == 0 || indeksAktualnegoSladu >= sladyGracza.slady.Count)
            return;

        // Aktualna pozycja celu (śladu gracza)
        Vector3 pozycjaCelu = sladyGracza.slady[indeksAktualnegoSladu];
        Vector3 kierunek = (pozycjaCelu - transform.position).normalized;

        // Przesuwa przeciwnika w kierunku aktualnego punktu ścieżki
        transform.position += kierunek * predkosc * Time.deltaTime;

        // Sprawdza, czy przeciwnik zbliżył się do aktualnego celu
        if (Vector3.Distance(transform.position, pozycjaCelu) < minimalnaOdleglosc)
        {
            indeksAktualnegoSladu++; // Przechodzi do następnego punktu
        }

        // Jeśli przeciwnik jest blisko gracza, kończy grę
        if (indeksAktualnegoSladu >= sladyGracza.slady.Count - 1 &&
            Vector3.Distance(transform.position, sladyGracza.transform.position) < minimalnaOdleglosc)
        {
            KoniecGry();
        }
    }

    private void KoniecGry()
    {
        Debug.Log("Koniec Gry! Przeciwnik złapał gracza.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
