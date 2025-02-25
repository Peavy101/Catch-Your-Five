using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public void MoveHook(float targetY)
    {
        // Get the current position and keep X and Z intact
        Vector3 currentPosition = transform.position;

        // Set the new position with the target Y value
        transform.position = new Vector3(currentPosition.x, (targetY - 0.1f), currentPosition.z);
    }
}
