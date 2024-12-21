using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public TMP_Text healthText; // Reference to the TextMeshPro component
    public GameObject gameOverUI; // Reference to the Game Over UI GameObject

    public delegate void OnHealthChanged(int currentHealth, int maxHealth);
    public event OnHealthChanged onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize the health display
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (onHealthChanged != null)
        {
            onHealthChanged.Invoke(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (onHealthChanged != null)
        {
            onHealthChanged.Invoke(currentHealth, maxHealth);
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}%";
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        // Pause the game
        Time.timeScale = 0f;

        // Show Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            Debug.LogError("Game Over UI reference is not assigned in HealthSystem!");
        }

        // Stop the music
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic();
        }
        else
        {
            Debug.LogError("MusicManager not found in the scene!");
        }
    }
}