using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject hiddenObject; // The hidden object to open (e.g., a pause menu)
    public AudioSource musicAudioSource; // Reference to the AudioSource playing the music
    public bool isPaused = false;   // Tracks if the game is paused

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame() // Made public so it can be accessed externally
    {
        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;

        // Show the hidden object
        if (hiddenObject != null)
        {
            hiddenObject.SetActive(true);
        }

        // Pause the music
        if (musicAudioSource != null)
        {
            musicAudioSource.Pause();
        }
    }

    public void ResumeGame() // Made public so it can be accessed externally
    {
        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;

        // Hide the hidden object
        if (hiddenObject != null)
        {
            hiddenObject.SetActive(false);
        }

        // Resume the music
        if (musicAudioSource != null)
        {
            musicAudioSource.UnPause();
        }
    }
}