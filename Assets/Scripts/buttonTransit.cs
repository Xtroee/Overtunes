using UnityEngine;

public class UIObjectAppearTransition : MonoBehaviour
{
    public RectTransform objectToAppear; // The UI object to appear
    public AudioSource soundEffect;      // The sound effect to play when the object appears
    public float appearTime = 5f;        // Time in seconds to wait before the object appears
    public float transitionDuration = 1f; // Duration of the transition in seconds

    [Header("Position Settings")]
    public Vector2 startPosition;        // Starting position of the object (outside the screen)
    public Vector2 targetPosition;       // Target position of the object (inside the screen)

    [Header("Scale Settings")]
    public Vector3 startScale = Vector3.zero; // Starting scale (e.g., 0 for scaling up)
    public Vector3 targetScale = Vector3.one;  // Target scale (e.g., 1 for full size)

    private float timer = 0f;            // Timer to track the elapsed time
    private bool isObjectVisible = false; // Tracks if the object is currently visible
    private bool isTransitioning = false; // Tracks if the object is currently transitioning

    void Start()
    {
        // Set the object's initial position and scale
        if (objectToAppear != null)
        {
            objectToAppear.anchoredPosition = startPosition;
            objectToAppear.localScale = startScale;
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

        // Handle the transition for appearing
        if (isTransitioning)
        {
            // Lerp the object's position from start to target
            float t = (timer - appearTime) / transitionDuration;
            objectToAppear.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            // Lerp the object's scale from start to target
            objectToAppear.localScale = Vector3.Lerp(startScale, targetScale, t);

            // Stop transitioning when the object reaches the target position and scale
            if (t >= 1f)
            {
                isTransitioning = false;
                isObjectVisible = true;
            }
        }
    }

    void MakeObjectAppear()
    {
        // Enable the object
        if (objectToAppear != null)
        {
            objectToAppear.gameObject.SetActive(true);

            // Play the sound effect
            if (soundEffect != null)
            {
                soundEffect.Play();
            }

            // Start the transition
            isTransitioning = true;
        }
    }
}