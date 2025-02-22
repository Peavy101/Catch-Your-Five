using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject fisherman;
    public Rigidbody2D fishermanRb;
    public GameObject boat;
    public Rigidbody2D boatRb;
    public Animator fishermanAnimator;
    public Animator boatAnimator;
    public AudioSource titleScreenSounds;  // Correct variable name (lowercase 't' for consistency)
    public AudioClip[] audioClips;
    public GameObject startText;
    public ParticleSystem boatParticles;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startText.SetActive(false);
            // Call GameStart method when the spacebar is pressed
            StartCoroutine(GameStart());
        }
    }

    private IEnumerator GameStart()
    {
        titleScreenSounds.clip = audioClips[0];
        titleScreenSounds.Play();
        yield return new WaitForSeconds(0.5f);
        fisherman.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        fishermanAnimator.SetBool("Walking", true);

        Transform fishermanTransform = fisherman.transform;

        titleScreenSounds.clip = audioClips[2];
        titleScreenSounds.Play();

        while (fishermanTransform.position.y > -2.71f)
        {
            fishermanRb.velocity = new Vector2(-1f, -1f);
            yield return null; // Wait for the next frame
        }

        // Stop vertical movement and move left
        fishermanRb.velocity = new Vector2(-1.5f, 0f);

        // Wait until fisherman reaches x = -0.92
        while (fishermanTransform.position.x > -0.92f)
        {
            yield return null; // Keep waiting until condition is met
        }

        fishermanAnimator.SetBool("Walking", false);
        fishermanRb.velocity = new Vector2(0f, 0f);
        titleScreenSounds.clip = audioClips[0];
        titleScreenSounds.Play();

        yield return new WaitForSeconds(0.5f);
        fisherman.SetActive(false);

        // Wait for another second before continuing
        yield return new WaitForSeconds(1f);

        titleScreenSounds.clip = audioClips[1];
        titleScreenSounds.Play();
        boatAnimator.SetBool("MotorOn", true);
        yield return new WaitForSeconds(0.25f);
        boatParticles.Play();

        yield return new WaitForSeconds(2f);

        boatRb.velocity = new Vector2(-1.1f, 0f);
        FindObjectOfType<TitleTextFade>().FadeOut();
    }
}

