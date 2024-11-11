using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Czu�o�� myszy
    public Transform cameraTransform; // Referencja do kamery (np. dziecko gracza na wysoko�ci g�owy)

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Ukrycie kursora
    }

    void Update()
    {
        // Pobieranie wej�cia myszy
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Obr�t w osi pionowej (g�ra/d�) i ograniczenie k�ta patrzenia
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Ustawienie rotacji kamery w osi pionowej
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Obr�t gracza w osi poziomej (lewo/prawo)
        transform.Rotate(Vector3.up * mouseX);
    }
}
