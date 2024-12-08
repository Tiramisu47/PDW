using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f; // Czas trwania wstrz�su
    public float shakeMagnitude = 0.2f; // Intensywno�� wstrz�su

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    private System.Collections.IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition; // Powr�t do pozycji pocz�tkowej
    }
}
