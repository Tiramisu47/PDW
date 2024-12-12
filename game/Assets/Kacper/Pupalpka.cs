using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Vector3 teleportLocation; // Miejsce, do którego gracz zostanie przeniesiony

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzenie, czy obiekt to gracz (mo¿na dodaæ tag "Player" do gracza i u¿yæ tego sprawdzenia)
        if (other.CompareTag("Player"))
        {
            // Znalezienie PlayerCamera w hierarchii gracza, zaczynaj¹c od g³ównego obiektu PlayerXCamera
            PlayerCamera playerCamera = other.transform.root.GetComponentInChildren<PlayerCamera>();

            if (playerCamera != null)
            {
                // Obliczenie kierunku, w którym patrzy gracz (kierunek osi Z w lokalnym uk³adzie wspó³rzêdnych kamery)
                Vector3 playerLookDirection = playerCamera.transform.forward;

                // Obliczenie kierunku ruchu gracza przez pu³apkê (po³o¿enie gracza minus po³o¿enie pu³apki)
                Vector3 playerMoveDirection = (other.transform.position - transform.position).normalized;

                // Obliczenie k¹ta pomiêdzy kierunkiem patrzenia a kierunkiem ruchu
                float angle = Vector3.Angle(playerLookDirection, playerMoveDirection);

                if (angle > 90f) // Jeœli k¹t jest wiêkszy ni¿ 90 stopni, to gracz idzie ty³em (patrzy w przeciwn¹ stronê)
                {
                    Debug.Log("Gracz przeszed³ ty³em - brak akcji.");
                }
                else
                {
                    Debug.Log("Gracz przeszed³ normalnie - teleportacja!");
                    other.transform.position = teleportLocation; // Teleportacja gracza
                }
            }
            else
            {
                Debug.LogWarning("Nie znaleziono PlayerCamera w hierarchii obiektu gracza.");
            }
        }
    }
}
