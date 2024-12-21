using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject projectilePrefab;  // The enemy's projectile prefab
    public Transform player;            // Reference to the player
    public float moveSpeed = 3f;        // Speed of the enemy's movement
    public float moveRange = 5f;        // Maximum distance the enemy can move in a direction
    public float fireRate = 1f;         // Time between shots (seconds)
    public float projectileSpeed = 7f; // Speed of the projectile
    public float projectileLifetime = 2f; // Lifetime of the projectile before being destroyed
    public int spreadCount = 5;         // Number of bullets to spread
    public float spreadAngle = 45f;     // Total angle range for the bullet spread

    private Vector3 initialPosition;    // Starting position of the enemy
    private Vector3 targetPosition;     // The target position the enemy is moving towards
    private float fireCooldown = 0f;    // Timer for firing projectiles

    void Start()
    {
        initialPosition = transform.position; // Store the initial position
        SetRandomTargetPosition();           // Set the first random target position
    }

    void Update()
    {
        // Move towards the target position
        MoveRandomly();

        // Handle projectile firing
        HandleFiring();
    }

    private void MoveRandomly()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If the enemy reaches the target position, set a new random position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-moveRange, moveRange);
        float randomY = Random.Range(-moveRange, moveRange);
        targetPosition = initialPosition + new Vector3(randomX, randomY, 0f);
    }

    private void HandleFiring()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f && player != null)
        {
            FireProjectileSpread();
            fireCooldown = fireRate; // Reset the cooldown
        }
    }

    private void FireProjectileSpread()
    {
        if (projectilePrefab != null)
        {
            // Calculate the central direction to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Calculate the angle offset for the spread
            float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            float halfSpread = spreadAngle / 2f;

            for (int i = 0; i < spreadCount; i++)
            {
                // Calculate the angle for this specific bullet
                float bulletAngle = baseAngle - halfSpread + (i * spreadAngle / (spreadCount - 1));
                Vector3 bulletDirection = new Vector3(Mathf.Cos(bulletAngle * Mathf.Deg2Rad), Mathf.Sin(bulletAngle * Mathf.Deg2Rad), 0f);

                // Instantiate and launch the bullet
                GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = bulletDirection * projectileSpeed;
                }

                // Destroy the bullet after a set lifetime
                Destroy(bullet, projectileLifetime);
            }
        }
    }
}
