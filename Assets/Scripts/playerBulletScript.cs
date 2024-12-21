using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public System.Action OnHitEnemy; // Callback for hitting an enemy
    public System.Action OnMiss;    // Callback for missing
    public float hitWindow = 2.5f;  // Time window to consider a hit valid
    public GameObject PBulletParticlePrefab; // Prefab for the particle effect

    private float travelTime = 0f;  // Time since the bullet was fired

    private void Update()
    {
        travelTime += Time.deltaTime;

        // If bullet exists beyond the hit window, it's a miss
        if (travelTime > hitWindow)
        {
            OnMiss?.Invoke(); // Trigger miss logic
            Destroy(gameObject); // Destroy the bullet
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Ensure the target has the "Enemy" tag
        {
            if (travelTime <= hitWindow)
            {
                OnHitEnemy?.Invoke(); // Trigger scoring logic for a valid hit
            }
            else
            {
                OnMiss?.Invoke(); // Trigger miss logic for late hit
            }

            // Instantiate the particle effect at the bullet's position
            if (PBulletParticlePrefab != null)
            {
                GameObject particleInstance = Instantiate(PBulletParticlePrefab, transform.position, Quaternion.identity);
                ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();

                    // Destroy the particle system after it finishes playing
                    Destroy(particleInstance, particleSystem.main.duration);
                }
            }

            Destroy(gameObject); // Destroy the bullet
        }
    }
}