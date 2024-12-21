using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource; // Reference to the AudioSource component
    public GameObject stageClearUI; // Reference to the UI element for "Stage Clear"

    private bool isGameOver = false; // Flag to track if the game is over

    void Start()
    {
        // Ensure the music source is assigned
        if (musicSource == null)
        {
            Debug.LogError("MusicManager: AudioSource is not assigned!");
            return;
        }

        // Play the music
        musicSource.Play();
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Check if the music has finished playing and the game is not over
        if (!isGameOver && !musicSource.isPlaying && musicSource.time >= musicSource.clip.length)
        {
            // Trigger "Stage Clear"
            StageClear();
        }
    }

    void StageClear()
    {
        Debug.Log("Stage Clear!");

        // Enable the "Stage Clear" UI (if assigned)
        if (stageClearUI != null)
        {
            stageClearUI.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    // Method to stop the music and prevent "Stage Clear"
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        // Set the game over flag to prevent "Stage Clear" from triggering
        isGameOver = true;
    }
}