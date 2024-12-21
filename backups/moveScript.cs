using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerScript : MonoBehaviour
{
    // Player movement settings
    public float speed = 5.0f;
    public float acceleration = 10f; // Acceleration for smooth movement
    public float dashSpeed = 10f; // Dash speed multiplier
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashCooldown = 3f; // Cooldown for dashing
    public Animator animator;
    public string horizontal = "Horizontal";
    public string vertical = "Vertical";

    // Crosshair and shooting settings
    public GameObject crossHair;
    public GameObject playerBulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;
    public float bulletLifetime = 2.0f;

    // Score and multiplier
    public TextMeshProUGUI scoreText; // Reference to a TextMeshProUGUI component for displaying score
    private int score = 0; // Current score
    private int multiplier = 1; // Current multiplier

    private float fireCooldown = 0f;
    private float dashCooldownTimer = 0f; // Timer for dash cooldown
    private bool isDashing = false; // Flag for dashing
    private Rigidbody2D rb;
    private Vector2 targetVelocity; // Desired velocity based on input

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on the Player GameObject!");
        }

        UpdateScoreText();
    }

    void Update()
    {
        HandleMovement();
        MoveCrosshair();
        HandleFiring();
        HandleDash();
    }

    private void HandleMovement()
    {
        if (!isDashing) // Only allow normal movement if not dashing
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            // Calculate the target velocity
            targetVelocity = new Vector2(horizontalInput, verticalInput).normalized * speed;

            // Smoothly transition to the target velocity
            rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, acceleration * Time.deltaTime);

            // Update animator parameters
            if (animator != null)
            {
                animator.SetFloat(horizontal, horizontalInput);
                animator.SetFloat(vertical, verticalInput);
            }
        }
    }

    private void HandleDash()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f) // Dash on Space press
        {
            StartCoroutine(Dash());
            dashCooldownTimer = dashCooldown; // Reset cooldown timer
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        // Store the current velocity and apply the dash speed
        Vector2 dashDirection = rb.velocity.normalized; // Get the current movement direction
        rb.velocity = dashDirection * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Reset the velocity after the dash is complete
        rb.velocity = Vector2.zero; // Optionally reset the velocity to zero or set it back to normal
        isDashing = false; // End the dash
    }

    private void MoveCrosshair()
    {
        if (crossHair != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = crossHair.transform.position.z;
            crossHair.transform.position = worldPosition;
        }
        else
        {
            Debug.LogError("CrossHair GameObject is not assigned.");
        }
    }

    private void HandleFiring()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            FireBullet();
            fireCooldown = fireRate;
        }
    }

    private void FireBullet()
    {
        if (playerBulletPrefab != null)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);

            Vector3 direction = (crossHair.transform.position - transform.position).normalized;

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = direction * bulletSpeed;
                Debug.Log("Bullet fired with velocity: " + direction * bulletSpeed);
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a Rigidbody2D component!");
            }

            // Assign scoring callbacks to the projectile
            ProjectileScript projectileScript = bullet.AddComponent<ProjectileScript>();
            projectileScript.OnHitEnemy = () => OnProjectileHitEnemy();
            projectileScript.OnMiss = OnProjectileMiss;
            projectileScript.hitWindow = 2.5f; // Set hit window for the projectile

            Destroy(bullet, bulletLifetime);
        }
        else
        {
            Debug.LogError("Player bullet prefab is not assigned.");
        }
    }


    private void OnProjectileHitEnemy()
    {
        multiplier++;
        score += multiplier;
        UpdateScoreText();
    }

    private void OnProjectileMiss()
    {
        multiplier = 1; // Reset multiplier
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score}\nx{multiplier}";
        }
        else
        {
            Debug.LogError("Score TextMeshProUGUI is not assigned!");
        }
    }
}
