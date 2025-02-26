using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isHooked = false;
    private FishingLine fishingLine;

    public float wiggleStrength = 0.1f; // How much the fish moves side to side when struggling
    public float pullStrength = 1.5f; // How hard the fish pulls down
    public float struggleSpeed = 3f; // How fast it wiggles when hooked

    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Base scale influenced by Y position (lower fish = bigger)
        float baseScale = Mathf.Clamp(1.5f - transform.position.y * 0.1f, 0.5f, 1.5f);

        // Add randomness to scale
        float randomFactor = Random.Range(0.8f, 1.2f); // Random variation (±20%)
        float finalScale = baseScale * randomFactor;

        // Apply scale uniformly (X & Y)
        transform.localScale = new Vector3(finalScale, finalScale, 1f);

        Debug.Log($"Fish spawned at Y: {transform.position.y}, Base Scale: {baseScale}, Random Factor: {randomFactor}, Final Scale: {finalScale}");

        StartCoroutine(FishBehaviour());
    }


    void Update()
    {
        if (isHooked)
        {
            PullLineDown();
            Wriggle();
        }
    }

    private void PullLineDown()
    {
        // Fish applies downward force to resist being reeled in
        fishingLine.ApplyDownwardForce(pullStrength * Time.deltaTime);
    }

    private void Wriggle()
    {
        // Makes the fish wiggle side to side while hooked
        transform.position += new Vector3(Mathf.Sin(Time.time * struggleSpeed) * wiggleStrength * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook") && !isHooked)
        {
            isHooked = true;
            FindObjectOfType<FishingLine>().HookFish(1f); // Hook the fish
            fishingLine = other.GetComponentInParent<FishingLine>(); // Get reference to FishingLine
            rb.velocity = Vector2.zero; // Stop movement when caught
            rb.isKinematic = true; // Prevents physics from affecting fish
        }
    }

    private IEnumerator FishBehaviour()
    {
        while (!isHooked)
        {
            // Fish moves randomly before being caught
            rb.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-0.5f, 0.5f));
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}
