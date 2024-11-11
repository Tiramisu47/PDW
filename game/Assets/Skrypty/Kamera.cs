using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Czu³oœæ myszy
    public Transform cameraTransform; // Referencja do kamery (np. dziecko gracza na wysokoœci g³owy)

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Ukrycie kursora
    }

    void Update()
    {
        // Pobieranie wejœcia myszy
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Obrót w osi pionowej (góra/dó³) i ograniczenie k¹ta patrzenia
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Ustawienie rotacji kamery w osi pionowej
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Obrót gracza w osi poziomej (lewo/prawo)
        transform.Rotate(Vector3.up * mouseX);
    }
}
