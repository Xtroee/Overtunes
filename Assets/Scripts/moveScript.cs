using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerScript : MonoBehaviour
{
    // Player movement settings
    public float speed = 5.0f;
    public float acceleration = 50f; // Acceleration for smooth movement
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
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int multiplier = 1;

    private float fireCooldown = 0f;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;
    private Rigidbody2D rb;
    private Vector2 targetVelocity;

    // Reference to HealthSystem
    public HealthSystem healthSystem;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on the Player GameObject!");
        }

        UpdateScoreText();
        Cursor.visible = false;
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
        if (!isDashing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            targetVelocity = new Vector2(horizontalInput, verticalInput).normalized * speed;
            rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, acceleration * Time.deltaTime);

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

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f)
        {
            StartCoroutine(Dash());
            dashCooldownTimer = dashCooldown;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        Vector2 dashDirection = rb.velocity.normalized;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        isDashing = false;
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
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a Rigidbody2D component!");
            }

            ProjectileScript projectileScript = bullet.AddComponent<ProjectileScript>();
            projectileScript.OnHitEnemy = () => OnProjectileHitEnemy();
            projectileScript.OnMiss = OnProjectileMiss;
            projectileScript.hitWindow = 1.75f;

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

        CheckMultiplierThreshold();
    }

    private void OnProjectileMiss()
    {
        multiplier = 1;
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

    //private int lastThreshold = 0; // To track the last reached threshold
    private void CheckMultiplierThreshold()
    {
        // Check if multiplier of x
        if (multiplier % 5 == 0 && multiplier > 0)
        {
            if (healthSystem != null)
            {
                Debug.Log("Healed");
                healthSystem.Heal(3); // Heal by 5 points
            }
        }
    }
}
