using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Vector3 teleportLocation; // Miejsce, do kt�rego gracz zostanie przeniesiony

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzenie, czy obiekt to gracz (mo�na doda� tag "Player" do gracza i u�y� tego sprawdzenia)
        if (other.CompareTag("Player"))
        {
            // Znalezienie PlayerCamera w hierarchii gracza, zaczynaj�c od g��wnego obiektu PlayerXCamera
            PlayerCamera playerCamera = other.transform.root.GetComponentInChildren<PlayerCamera>();

            if (playerCamera != null)
            {
                // Obliczenie kierunku, w kt�rym patrzy gracz (kierunek osi Z w lokalnym uk�adzie wsp�rz�dnych kamery)
                Vector3 playerLookDirection = playerCamera.transform.forward;

                // Obliczenie kierunku ruchu gracza przez pu�apk� (po�o�enie gracza minus po�o�enie pu�apki)
                Vector3 playerMoveDirection = (other.transform.position - transform.position).normalized;

                // Obliczenie k�ta pomi�dzy kierunkiem patrzenia a kierunkiem ruchu
                float angle = Vector3.Angle(playerLookDirection, playerMoveDirection);

                if (angle > 90f) // Je�li k�t jest wi�kszy ni� 90 stopni, to gracz idzie ty�em (patrzy w przeciwn� stron�)
                {
                    Debug.Log("Gracz przeszed� ty�em - brak akcji.");
                }
                else
                {
                    Debug.Log("Gracz przeszed� normalnie - teleportacja!");
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
