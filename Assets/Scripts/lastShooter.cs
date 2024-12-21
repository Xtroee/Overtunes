using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedShooter : MonoBehaviour
{
    public GameObject bulletPrefab;      // The bullet prefab to be fired
    public Transform target;            // Reference to the target (e.g., the player)
    public float shootInterval = 1.5f;  // Time interval between shots
    public float bulletSpeed = 8f;      // Speed at which the bullets are fired
    public float bulletDuration = 2f;   // How long the bullet lasts before being destroyed
    public int bulletSpread = 3;        // Number of bullets to fire in a spread
    public float spreadWidth = 60f;     // Angle range of the bullet spread

    private float shootCooldown = 0f;   // Timer for shooting cooldown
    private float startTime;            // Time when the game started

    void Start()
    {
        // Record the start time of the game
        startTime = Time.time;
    }

    void Update()
    {
        // Manage the shooting logic
        PerformShooting();
    }

    private void PerformShooting()
    {
        // Calculate the current game time
        float currentTime = Time.time - startTime;

        // Check if the current time is between 10 seconds and 200 seconds
        if (currentTime >= 60f && currentTime <= 80f)
        {
            shootCooldown -= Time.deltaTime;

            if (shootCooldown <= 0f && target != null)
            {
                LaunchBulletSpread();
                shootCooldown = shootInterval; // Reset the shooting cooldown
            }
        }
    }

    private void LaunchBulletSpread()
    {
        if (bulletPrefab != null)
        {
            // Determine the main direction toward the target
            Vector3 targetDirection = (target.position - transform.position).normalized;

            // Compute the base angle
            float mainAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            float angleOffset = spreadWidth / 2f;

            for (int i = 0; i < bulletSpread; i++)
            {
                // Calculate the angle for each bullet
                float currentAngle = mainAngle - angleOffset + (i * spreadWidth / (bulletSpread - 1));
                Vector3 bulletDirection = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0f);

                // Create and launch the bullet
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = bulletDirection * bulletSpeed;
                }

                // Schedule the bullet for destruction
                Destroy(bullet, bulletDuration);
            }
        }
    }
}