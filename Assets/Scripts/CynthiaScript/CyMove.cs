using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 3f;        // Speed of the enemy's movement
    public float movementRange = 5f;        // Maximum distance the enemy can move in a direction

    private Vector3 startingPosition;       // Starting position of the enemy
    private Vector3 destinationPosition;    // The target position the enemy is moving towards

    void Start()
    {
        startingPosition = transform.position; // Store the initial position
        SetRandomDestination();               // Set the first random target position
    }

    void Update()
    {
        // Move towards the target position
        MoveRandomly();
    }

    private void MoveRandomly()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, movementSpeed * Time.deltaTime);

        // If the enemy reaches the target position, set a new random position
        if (Vector3.Distance(transform.position, destinationPosition) < 0.1f)
        {
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        float randomX = Random.Range(-movementRange, movementRange);
        float randomY = Random.Range(-movementRange, movementRange);
        destinationPosition = startingPosition + new Vector3(randomX, randomY, 0f);
    }
}