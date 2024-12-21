using System.Collections;
using UnityEngine;


public class ScreenShake : MonoBehaviour
{
    [Header("Camera Shake Settings")]
    public Camera mainCamera; // Assign your camera in the Inspector
    public float shakeStrength = 0.5f; // Maximum shake intensity
    public float shakeFade = 1f; // How quickly the shake fades
    public float stableShake = 0.1f; // Minimum shake intensity

    private Vector3 originalPosition; // To store the camera's original position

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Fallback to the main camera
        }

        if (mainCamera != null)
        {
            originalPosition = mainCamera.transform.localPosition;
        }
        else
        {
            Debug.LogError("Main Camera is not assigned!");
        }
    }

    // Call this method to trigger the screen shake
    public void TriggerShake(float duration)
    {
        if (mainCamera != null)
        {
            StartCoroutine(Shake(duration));
        }
    }

    private IEnumerator Shake(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float currentShakeStrength = Mathf.Lerp(shakeStrength, stableShake, elapsed / duration);

            // Generate random offsets for the camera's position
            float offsetX = Random.Range(-1f, 1f) * currentShakeStrength;
            float offsetY = Random.Range(-1f, 1f) * currentShakeStrength;

            // Apply the offset to the camera's position
            mainCamera.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restore the camera's original position
        mainCamera.transform.localPosition = originalPosition;
    }
}
