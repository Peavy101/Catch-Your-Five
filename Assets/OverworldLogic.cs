using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldLogic : MonoBehaviour
{
    public GameObject Flock;
    public Camera camera; // Reference to the camera
    public GameObject[] Objects; // Objects to check for leaving the camera bounds

    void SeagullFlyby()
    {
        HandleDespawn(Flock);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void HandleDespawn(GameObject Object)
    {
        Camera OverworldCam = Camera.main;

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
