using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementWithCamera : MonoBehaviour
{
    public float speed = 5f; // Prędkość poruszania się
    public float jumpHeight = 2f; // Wysokość skoku
    public float gravity = -9.81f; // Grawitacja
    public Transform cameraTransform; // Transformacja kamery, która wyznacza kierunek

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        // Pobieramy komponent CharacterController
        controller = GetComponent<CharacterController>();

        // Sprawdzamy, czy kamera została przypisana
        if (cameraTransform == null)
        {
            Debug.LogError("Nie przypisano Transformacji Kamery! Przypisz kamerę do pola cameraTransform w inspektorze.");
        }
    }

    void Update()
    {
        if (cameraTransform == null) return; // Jeśli kamera nie jest przypisana, zatrzymujemy działanie skryptu

        // Sprawdzanie, czy gracz stoi na ziemi
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Pobieranie wejścia gracza
        float moveX = Input.GetAxis("Horizontal"); // Poruszanie się w lewo/prawo
        float moveZ = Input.GetAxis("Vertical");   // Poruszanie się w przód/tył

        // Wyznaczanie kierunku poruszania się względem kierunku kamery
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ustawienie kierunku na poziomie (ignoring Y), aby uniknąć poruszania się w osi Y
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Obliczamy wektor ruchu gracza w płaszczyźnie XZ, zgodnie z kierunkiem kamery
        Vector3 move = (forward * moveZ + right * moveX).normalized;
        controller.Move(move * speed * Time.deltaTime);

        // Obsługa skoku
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Dodanie grawitacji
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
