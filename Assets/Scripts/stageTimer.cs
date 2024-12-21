using UnityEngine;
using TMPro;

public class MusicSyncedTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro Text component
    public AudioSource musicAudioSource; // Reference to the AudioSource playing the music

    private float totalTime = 133f; // Total time in seconds (2 minutes and 13 seconds)
    private bool timerIsRunning = false;

    void Start()
    {
        // Check if the AudioSource is assigned
        if (musicAudioSource == null)
        {
            Debug.LogError("Music AudioSource is not assigned!");
            return;
        }

        // Start the timer when the music starts playing
        musicAudioSource.Play();
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            // Calculate the remaining time based on the music's playback time
            float timeRemaining = totalTime - musicAudioSource.time;

            if (timeRemaining > 0)
            {
                // Display the time in minutes and seconds format
                DisplayTime(timeRemaining);
            }
            else
            {
                // If the timer reaches 0, stop it
                timeRemaining = 0;
                timerIsRunning = false;
                timerText.text = "Clear!";
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Calculate minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the TextMeshPro text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}