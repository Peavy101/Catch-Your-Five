using UnityEngine;

public class FishingLine : MonoBehaviour
{
    public float stretchSpeed = 2f;
    public float maxHeight = 5f;
    public float minHeight = 1f;
    private bool left = false;
    private bool hooked = false;
    private float fishForce;

    public CircleCollider2D hookCollider; // Hook collider

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (!hooked)
        {
            Fishing();
        }
        else
        {
            Fishing();
            ApplyDownwardForce(fishForce);
        }
    }

    public void HookFish(float fishSize)
    {
        Debug.Log("Fish hooked! Size: " + fishSize);
        fishForce = fishSize * 1.5f; // Scale pull force based on fish size
        hooked = true;
    }

    private void Fishing()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float newHeight = Mathf.Min(transform.localScale.y + stretchSpeed * Time.deltaTime, maxHeight);
            float heightDifference = newHeight - transform.localScale.y;
            transform.localScale = new Vector3(originalScale.x, newHeight, originalScale.z);
            transform.position -= new Vector3(0, heightDifference / 2, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && left)
        {
            PullUp();
            left = false;
            DidICatchAFish();
        }
        if (Input.GetKey(KeyCode.RightArrow) && !left)
        {
            PullUp();
            left = true;
            DidICatchAFish();
        }
        UpdateLowestPoint();
    }

    private void PullUp()
    {
        float newHeight = Mathf.Max(transform.localScale.y - stretchSpeed * Time.deltaTime * 4, minHeight);
        float heightDifference = transform.localScale.y - newHeight;
        transform.localScale = new Vector3(originalScale.x, newHeight, originalScale.z);
        transform.position += new Vector3(0, heightDifference / 2, 0);
    }

    public void ApplyDownwardForce(float force)
    {
        float newHeight = Mathf.Min(transform.localScale.y + force * Time.deltaTime, maxHeight);
        float heightDifference = newHeight - transform.localScale.y;
        transform.localScale = new Vector3(originalScale.x, newHeight, originalScale.z);
        transform.position -= new Vector3(0, heightDifference / 2, 0);
    }

    void UpdateLowestPoint()
    {
        float lowestY = transform.position.y - transform.localScale.y / 2f;
        FindObjectOfType<Hook>().MoveHook(lowestY);
    }

    private void DidICatchAFish()
    {
        if (hookCollider == null) return; // Safety check

        Collider2D fishCollider = Physics2D.OverlapCircle(hookCollider.bounds.center, hookCollider.radius, ~0);

        if (fishCollider != null && fishCollider.CompareTag("Fish"))
        {
            float fishSize = fishCollider.transform.localScale.x; // Get fish size
            HookFish(fishSize);
        }
    }
}
