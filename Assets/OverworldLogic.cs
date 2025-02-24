using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldLogic : MonoBehaviour
{
    public GameObject FlockFab;
    public Camera OverworldCam; // Reference to the camera
    public List<GameObject> Objects; // Objects to check for leaving the camera bounds

    // Start is called before the first frame update
    void Start()
    {
    }

    void SpawnFlock()
    {
        Vector3 TLP = OverworldCam.ViewportToWorldPoint(new Vector3(0, 1, OverworldCam.nearClipPlane)); // Top-left world point
        Vector3 BRP = OverworldCam.ViewportToWorldPoint(new Vector3(1, 0, OverworldCam.nearClipPlane)); // Bottom-right world point

        float CamWidth = BRP.y - TLP.y;
        float CamHeight = TLP.y - BRP.y;

        GameObject FlockObj = Instantiate(FlockFab);

        BirdFlock Flock = FlockObj.AddComponent<BirdFlock>();

        Vector3 SpawnPoint = new Vector3(Flock.Direction * CamWidth / 2, BRP.y + CamHeight * 0.75f, 0);

        FlockObj.transform.position = SpawnPoint;

        Objects.Add(FlockObj);
    }

    public bool ObjOutOfBounds(GameObject Object)
    {
        Vector3 TLP = OverworldCam.ViewportToWorldPoint(new Vector3(0, 1, OverworldCam.nearClipPlane)); // Top-left world point
        Vector3 BRP = OverworldCam.ViewportToWorldPoint(new Vector3(1, 0, OverworldCam.nearClipPlane)); // Bottom-right world point
        Vector3 ObjectPos = OverworldCam.WorldToScreenPoint(Object.transform.position);

        // Check if the object's screen position is outside the bounds of the camera's view
        if (ObjectPos.x < TLP.x || ObjectPos.x > BRP.x || ObjectPos.y < BRP.y || ObjectPos.y > TLP.y)
        {
            // Object is out of bounds, so despawn it
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // Randomly spawn a seagull flock every now and den
        
        if (Random.Range(0, 10000) == 0) // tiny ahh chance of spawning each frame
        {
            // Max of one flock at a time
            if (FindObjectsOfType<BirdFlock>().Length == 0)
            {
                SpawnFlock();
            }
        }

        // Despawn the objects that move out of bounds
        //for (int i = Objects.Count - 1; i >= 0; i--)
        //{
        //    if (ObjOutOfBounds(Objects[i]))
        //    {
        //        Destroy(Objects[i]);
        //        Objects.RemoveAt(i);
        //    }
        //}
    }
}
