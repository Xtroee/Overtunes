using UnityEngine;

public class EnemyBulletCollider : MonoBehaviour
{
    public int damage = 2; // Amount of damage this bullet deals

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Playerhitbox"))
        {
            // Find the HealthSystem component on the hitBoxPlayer
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();

            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage); // Apply damage
            }

            Destroy(gameObject); // Destroy the projectile
        }
    }
}
