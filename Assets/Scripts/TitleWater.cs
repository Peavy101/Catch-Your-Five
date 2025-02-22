using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWater : MonoBehaviour
{
    public float amplitude = 0.5f; // Height of the bobbing motion
    public float frequency = 1f;   // Speed of the bobbing motion

    private float startPosY; // Store only the Y position

    void Start()
    {
        startPosY = transform.position.y; // Store only the Y coordinate
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, startPosY + yOffset, transform.position.z);
    }
}

