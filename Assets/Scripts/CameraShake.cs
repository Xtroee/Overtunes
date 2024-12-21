using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Duration of the shake effect
    public float shakeMagnitude = 0.2f; // Intensity of the shake effect

    private Vector3 originalPosition; // Original position of the camera

    void Start()
    {
        // Store the original position of the camera
        originalPosition = transform.localPosition;
    }

    // Public method to trigger the shake effect
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Generate a random offset within the shake magnitude
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Apply the offset to the camera's position
            transform.localPosition = originalPosition + new Vector3(x, y, 0f);

            // Increment the elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera to its original position
        transform.localPosition = originalPosition;
    }
}