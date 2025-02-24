using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldLogic : MonoBehaviour
{
    public Camera OverworldCam; // Reference to the camera
    public GameObject[] Objects; // Objects to check for leaving the camera bounds

    // Start is called before the first frame update
    void Start()
    {

    }

    void SpawnFlock()
    {
        Vector3 TLP = OverworldCam.ViewportToWorldPoint(new Vector3(0, 1, OverworldCam.nearClipPlane)); // Top-left world point
        Vector3 BRP = OverworldCam.ViewportToWorldPoint(new Vector3(1, 0, OverworldCam.nearClipPlane)); // Bottom-right world point

        float CamHeight = TLP.y - BRP.y;
        int Direction = Random.Range(0, 2) * 2 - 1;
        Vector3 SpawnPoint;

        BirdFlock Flock;

        if (Direction == 1)
        {
            SpawnPoint = new Vector3(TLP.x, BRP.y + 0.75f * CamHeight, TLP.z);
            Flock = new BirdFlock(SpawnPoint, Direction);
            Objects.append(BirdFlock.Flock);
        }
        else if (Direction == -1)
        {
            SpawnPoint = new Vector3(BRP.x, BRP.y + 0.75f * CamHeight, TLP.z);
            Flock = new BirdFlock(SpawnPoint, Direction);
            Objects.append(BirdFlock.Flock);
        }
    }

    public void HandleDespawn(GameObject Object)
    {
        Vector3 TLP = OverworldCam.ViewportToWorldPoint(new Vector3(0, 1, OverworldCam.nearClipPlane)); // Top-left world point
        Vector3 BRP = OverworldCam.ViewportToWorldPoint(new Vector3(1, 0, OverworldCam.nearClipPlane)); // Bottom-right world point

        Vector3 ObjectPos = OverworldCam.WorldToScreenPoint(Object.transform.position);

        // Check if the object's screen position is outside the bounds of the camera's view
        if (ObjectPos.x < TLP.x || ObjectPos.x > BRP.x || ObjectPos.y < BRP.y || ObjectPos.y > TLP.y)
        {
            // Object is out of bounds, so despawn it
            Destroy(Object);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
