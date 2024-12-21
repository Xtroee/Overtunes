using UnityEngine;

public class TimedObjectAppearance : MonoBehaviour
{
    public GameObject objectToAppear; // The object to appear and disappear
    public AudioSource soundEffect;   // The sound effect to play when the object appears
    public float appearTime = 5f;     // Time in seconds to wait before the object appears
    public float disappearTime = 10f; // Time in seconds to wait before the object disappears
    public float transitionDuration = 1f; // Duration of the sliding transition in seconds
    public ScreenShake screenShake;
    

    [Header("Position Settings")]
    public Vector3 startPosition;     // Starting position of the object (outside the screen)
    public Vector3 targetPosition;    // Target position of the object (inside the screen)
    public Vector3 endPosition;       // End position of the object (outside the screen for disappearing)

    private float timer = 0f;         // Timer to track the elapsed time
    private bool isObjectVisible = false; // Tracks if the object is currently visible
    private bool isTransitioning = false; // Tracks if the object is currently transitioning

    void Start()
    {
        // Set the object's initial position to the start position
        if (objectToAppear != null)
        {
            objectToAppear.transform.position = startPosition;
        }
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to make the object appear
        if (!isObjectVisible && timer >= appearTime && !isTransitioning)
        {
            MakeObjectAppear();
        }

        // Handle the sliding transition for appearing
        if (isTransitioning && timer < disappearTime)
        {
            // Lerp the object's position from start to target
            float t = (timer - appearTime) / transitionDuration;
            objectToAppear.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Stop transitioning when the object reaches the target position
            if (t >= 1f)
            {
                isTransitioning = false;
            }
        }

        // Check if it's time to make the object disappear
        if (isObjectVisible && timer >= disappearTime)
        {
            MakeObjectDisappear();
        }

        // Handle the sliding transition for disappearing
        if (isObjectVisible && timer >= disappearTime && timer < disappearTime + transitionDuration)
        {
            // Lerp the object's position from target to end

            float t = (timer - disappearTime) / transitionDuration;
            objectToAppear.transform.position = Vector3.Lerp(targetPosition, endPosition, t);

            // Disable the object when the transition is complete
            if (t >= 1f)
            {
                objectToAppear.SetActive(false);
                isObjectVisible = false;
            }
        }
    }

    void MakeObjectAppear()
    {
        // Enable the object
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(true);
            isObjectVisible = true;

            // Play the sound effect
            if (soundEffect != null)
            {
                soundEffect.Play();
                screenShake.TriggerShake(0.5f);

            }

            // Start the sliding transition
            isTransitioning = true;
        }
    }

    void MakeObjectDisappear()
    {
        // Start the sliding transition for disappearing
        isTransitioning = true;
    }
}