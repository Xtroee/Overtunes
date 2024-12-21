using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collided with the arena or any other collider
        if (collision.gameObject.CompareTag("Collider"))
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Alternatively, if the collider is a trigger
        if (other.CompareTag("Collider"))
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
