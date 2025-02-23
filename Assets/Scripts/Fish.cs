using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private int timeTillNextAction = 1;
    private float speedMultiplier = 1f;
    private int direction = -1;
    private int previousDirection = -1;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResizeBasedOnPosition();
        SetRandomVariables();
        if (direction > 0)
        {
            previousDirection = -1;
        }
        StartCoroutine(FishBehaviour());
    }

    private void ResizeBasedOnPosition()
    {
        float scaleFactor = Mathf.Abs(transform.position.y);  // Get the absolute value of Y to avoid negative scale
        float maxScale = 5f;  // Set the maximum size the fish can reach
        float minScale = 1f;  // Set the minimum size the fish can reach

        // Calculate scale based on Y position (scaleFactor goes up as Y position goes down)
        float scale = Mathf.Lerp(minScale, maxScale, scaleFactor / 10f);  // Adjust "10f" for more/less sensitivity

        // Apply the scale only to the X and Y axis, not Z (unless needed)
        transform.localScale = new Vector3(scale, scale, 1f);  // Adjust Z if necessary
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
        speedMultiplier = Random.Range(0.5f, 1.5f);
        timeTillNextAction = Random.Range(1, 4);
    }
}