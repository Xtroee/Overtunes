using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    public ScreenShake screenShake;

    void Start()
    {
        if (screenShake == null)
        {
            Debug.LogError("ScreenShake script not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Shake the screen when Space is pressed
        {
            screenShake.TriggerShake(0.5f); // Trigger a 0.5-second shake
        }
    }
}
