using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isHooked = false;
    private FishingLine fishingLine;

    public GameObject hook;  // Reference to the hook
    public float wiggleStrength = 0.1f; // How much the fish moves side to side when struggling
    public float pullStrength = 1.5f; // How hard the fish pulls down
    public float struggleSpeed = 3f; // How fast it wiggles when hooked
    public float moveSpeed = 3f;  // Speed at which the fish moves towards the hook

    private Vector3 initialScale;

    void Start()
    {
        hook = GameObject.FindWithTag("Hook"); // Find the hook
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;


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
            MoveToHook();  // Move towards the hook
            Wriggle();  // Apply struggling wiggle when hooked
        }
    }

    private void MoveToHook()
    {
        // Move the fish towards the hook position
        if (hook != null)
        {
            // Move towards the hook
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void Wriggle()
    {
        // Apply rotation struggle effect (fish tilts left and right)
        float rotationAmount = Mathf.Sin(Time.time * struggleSpeed) * 15f;
        transform.rotation = Quaternion.Euler(0, 0, rotationAmount);
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook") && !isHooked)
        {
            isHooked = true;
            fishingLine = GameObject.FindWithTag("FishingLine").GetComponent<FishingLine>(); // Find the fishing line
            fishingLine.HookFish(1f); // Hook the fish

            rb.velocity = Vector2.zero; // Stop movement when caught
            rb.isKinematic = false; // Allow physics again
            rb.constraints = RigidbodyConstraints2D.None; // Unlock Z rotation for struggling effect
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
