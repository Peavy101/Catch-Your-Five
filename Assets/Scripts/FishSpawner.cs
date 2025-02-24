using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    //Spawn One
    //After a random amount of time, spawn another
    //Keep track of how many have been spawned
    //After a certain amount, stop spawning
    //public method to bring number down when one fish despawns
    private int fishCount = 0;
    // private int amountToSpawn = 1;
    public GameObject fishPrefab;
    void Start()
    {
        // amountToSpawn = Random.Range(1, 3);
        SpawnFish();
    }

    private void SpawnFish()
    {
        fishCount++;
        float randomY = Random.Range(-3.5f, 1.8f);
        float randomX = Random.Range(-3.2f, 0.5f);
        Vector3 spawnLocation = new Vector3(randomX, randomY, 0);
        Instantiate(fishPrefab, spawnLocation, Quaternion.identity);
        if (fishCount < 5)
        {
            StartCoroutine(SpawnNextFish());
        }
    }

    private IEnumerator SpawnNextFish()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        SpawnFish();
    }

    public void LowerCounter()
    {
        fishCount--;
    }
}
