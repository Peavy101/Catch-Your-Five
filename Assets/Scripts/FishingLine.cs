using UnityEngine;

public class FishingLine : MonoBehaviour
{
    public float stretchSpeed = 2f; // Speed of stretching
    public float maxHeight = 5f; // Maximum height the object can stretch to (in local scale)
    public float minHeight = 1f; // Minimum height of the object (initial height)
    private bool left = false;

    private Vector3 originalScale;

    void Start()
    {
        // Save the original scale of the object
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float newHeight = Mathf.Min(transform.localScale.y + stretchSpeed * Time.deltaTime, maxHeight);
            float heightDifference = newHeight - transform.localScale.y;
            transform.localScale = new Vector3(originalScale.x, newHeight, originalScale.z);
            transform.position -= new Vector3(0, heightDifference / 2, 0); // Moves only downward
        }
        if (Input.GetKey(KeyCode.LeftArrow) && left)
        {
            float newHeight = Mathf.Max(transform.localScale.y - stretchSpeed * Time.deltaTime * 2, minHeight);
            float heightDifference = transform.localScale.y - newHeight;
            transform.localScale = new Vector3(originalScale.x, newHeight, originalScale.z);
            transform.position += new Vector3(0, heightDifference / 2, 0); // Moves only upward
            left = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !left)
        {
            float newHeight = Mathf.Max(transform.localScale.y - stretchSpeed * Time.deltaTime * 2, minHeight);
            float heightDifference = transform.localScale.y - newHeight;
            transform.localScale = new Vector3(originalScale.x, newHeight, originalScale.z);
            transform.position += new Vector3(0, heightDifference / 2, 0); // Moves only upward
            left = true;
        }
        // else if (Input.GetKey(KeyCode.UpArrow)) // Optional: retracting the line
        // {
        //     float newHeight = Mathf.Max(transform.localScale.y - stretchSpeed * Time.deltaTime, minHeight);
        //     float heightDifference = transform.localScale.y - newHeight;
        //     transform.localScale = new Vector3(originalScale.x, newHeight, originalScale.z);
        //     transform.position += new Vector3(0, heightDifference / 2, 0); // Moves only upward
        // }
    }
}
