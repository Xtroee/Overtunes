using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedShooterV2 : MonoBehaviour
{
    public GameObject projectilePrefab;      // The projectile prefab to be fired
    public Transform aimTarget;             // Reference to the target (e.g., the player)
    public float fireInterval = 1.5f;       // Time interval between shots (in seconds)
    public float projectileSpeed = 8f;      // Speed at which the projectiles are fired
    public float projectileLifetime = 2f;   // How long the projectile lasts before being destroyed (in seconds)
    public int projectileSpread = 3;        // Number of projectiles to fire in a spread
    public float spreadRange = 60f;         // Angle range of the projectile spread

    [Header("Timing Settings")]
    [Tooltip("Time to start shooting (in seconds)")]
    public float startFireTime = 10f;       // Time to start shooting (in seconds)

    [Tooltip("Time to stop shooting (in seconds)")]
    public float stopFireTime = 200f;       // Time to stop shooting (in seconds)

    private float fireCooldown = 0f;        // Timer for shooting cooldown
    private float gameStartTime;            // Time when the game started

    void Start()
    {
        // Record the start time of the game
        gameStartTime = Time.time;
    }

    void Update()
    {
        // Manage the shooting logic
        HandleShooting();
    }

    private void HandleShooting()
    {
        // Calculate the current game time
        float currentTime = Time.time - gameStartTime;

        // Check if the current time is between the start and stop shooting times
        if (currentTime >= startFireTime && currentTime <= stopFireTime)
        {
            fireCooldown -= Time.deltaTime;

            if (fireCooldown <= 0f && aimTarget != null)
            {
                LaunchProjectileSpread();
                fireCooldown = fireInterval; // Reset the shooting cooldown
            }
        }
    }

    private void LaunchProjectileSpread()
    {
        if (projectilePrefab != null)
        {
            // Determine the main direction toward the target
            Vector3 targetDirection = (aimTarget.position - transform.position).normalized;

            // Compute the base angle
            float baseAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            float angleOffset = spreadRange / 2f;

            for (int i = 0; i < projectileSpread; i++)
            {
                // Calculate the angle for each projectile
                float projectileAngle = baseAngle - angleOffset + (i * spreadRange / (projectileSpread - 1));
                Vector3 projectileDirection = new Vector3(Mathf.Cos(projectileAngle * Mathf.Deg2Rad), Mathf.Sin(projectileAngle * Mathf.Deg2Rad), 0f);

                // Create and launch the projectile
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = projectileDirection * projectileSpeed;
                }

                // Schedule the projectile for destruction
                Destroy(projectile, projectileLifetime);
            }
        }
    }
}