using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private int timeTillNextAction = 1;
    private float speedMultiplier = 1.5f;
    private int direction;
    private int previousDirection = 1;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomVariables();
        previousDirection = direction;
        StartCoroutine(FishBehaviour());
    }

    // Store the previous direction, assuming initial direction is 1 (right)
    private IEnumerator FishBehaviour()
    {
        Debug.Log("FishBehaviour is being called...");
        yield return new WaitForSeconds(timeTillNextAction);

        // Check if the direction has changed
        if (direction != previousDirection)
        {
            // Flip only if the direction has changed
            Vector3 newScale = transform.localScale;
            newScale.x = newScale.x * -1; // Only flips X without changing Y or Z
            transform.localScale = newScale;

            previousDirection = direction; // Update previous direction to the current direction
        }

        // Update the velocity with the current direction and speed multiplier
        rb.velocity = new Vector2((1f * direction * speedMultiplier), rb.velocity.y);
        Debug.Log(direction + " " + speedMultiplier);
        previousDirection = direction;

        // Set random variables for the next action
        SetRandomVariables();

        // Call FishBehaviour again to continue the cycle
        StartCoroutine(FishBehaviour());
    }

    private void SetRandomVariables()
    {
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
        speedMultiplier = Random.Range(1, 3);
        timeTillNextAction = Random.Range(1, 4);
    }

    private void HandleDirection()
    {

    }
}