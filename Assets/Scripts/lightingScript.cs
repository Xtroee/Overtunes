using UnityEngine;

public class TimedLightController : MonoBehaviour
{
    public GameObject lightObject; // Reference to the Light GameObject (can be Freeform Light)

    [Header("Timed Events")]
    public float turnOnTime = 60f; // Time in seconds to turn the light ON
    public float turnOffTime = 120f; // Time in seconds to turn the light OFF

    [Header("Transition Settings")]
    public float transitionDuration = 2f; // Duration of the fade-in/fade-out transition in seconds

    private float elapsedTime = 0f; // Tracks the elapsed time
    private bool isLightOn = false; // Tracks the current state of the light
    private float transitionTimer = 0f; // Timer for the transition

    void Update()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        // Check if it's time to turn the light ON
        if (!isLightOn && elapsedTime >= turnOnTime && elapsedTime < turnOffTime)
        {
            StartTransition(true);
        }

        // Check if it's time to turn the light OFF
        if (isLightOn && elapsedTime >= turnOffTime)
        {
            StartTransition(false);
        }

        // Handle the transition
        if (transitionTimer > 0f)
        {
            transitionTimer -= Time.deltaTime;

            // If the transition is complete, toggle the light state
            if (transitionTimer <= 0f)
            {
                ToggleLight();
            }
        }
    }

    void StartTransition(bool turnOn)
    {
        // Start the transition timer
        transitionTimer = transitionDuration;

        // If the light is already in the desired state, do nothing
        if ((turnOn && isLightOn) || (!turnOn && !isLightOn))
        {
            transitionTimer = 0f;
            return;
        }

        // Enable or disable the light GameObject immediately
        lightObject.SetActive(turnOn);
    }

    void ToggleLight()
    {
        // Toggle the light state
        isLightOn = !isLightOn;

        // Disable the light GameObject after the transition
        if (!isLightOn)
        {
            lightObject.SetActive(false);
        }
    }
}