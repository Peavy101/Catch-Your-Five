using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject fisherman;
    public GameObject boat;
    public Animator fishermanAnimator;
    public Animator boatAnimator;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //enable fisherman
            //play door sound
            //wait 1 second
            //start animation
            //start movement sequence
            //Once arrived stop movement and animation
            //play door sound
            //disable disherman
            //play motor sound and enable motor animation
            //wait 1 second
            //boat moves to the left
            //screen transition to next scene
        }

        // private void StartGame()
        // {

        // }
    }
}
