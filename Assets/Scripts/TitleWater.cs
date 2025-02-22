using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWater : MonoBehaviour
{
    public float amplitude = 0.5f; // Height of the bobbing motion
    public float frequency = 1f;   // Speed of the bobbing motion

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}
