using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlock : MonoBehaviour
{
    public int Direction = 0; // 1 -> eastwards, -1 -> westwards   
    public float Speed;

    public BirdFlock(){}

    public void Start()
    {
        // 1 = Spawn left fly right, -1 = spawn right fly left
        Direction = Random.Range(0, 2) * 2 - 1;
        Speed = Direction * Random.Range(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 FlockTranslation = new Vector3(Direction * Speed * Time.deltaTime, 0, 0);
        transform.Translate(FlockTranslation);
    }
}
