using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlock : MonoBehaviour
{
    static public GameObject Flock;
    int Direction = 0; // 1 -> eastwards, -1 -> westwards
    float Velocity;

    public BirdFlock(Vector3 Position, int Direction)
    {
        Instantiate(Flock);

        this.Flock.SetActive(true);

        this.Direction = Direction;
        this.Flock.transform.position = Position;

        // 1 = Spawn left fly right, -1 = spawn right fly left
        this.Velocity = this.Direction * Random.Range(0.0f, 1.0f);
    }


    void FlockLogic()
    {
        Flock.transform.position = new Vector3(Flock.transform.position.x + Velocity, Flock.transform.position.y, Flock.transform.position.z);
        OverworldLogic Overworld = GameObject.FindObjectOfType<OverworldLogic>();
        Overworld.HandleDespawn(Flock);
    }

    // Update is called once per frame
    void Update()
    {
        if (Flock.activeSelf)
        {
            FlockLogic();
        }
    }
}
