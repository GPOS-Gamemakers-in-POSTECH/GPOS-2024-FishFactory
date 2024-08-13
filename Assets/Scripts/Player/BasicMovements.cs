// script to control player movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovements : ActionPoints
{
    // aminator to control player animation
    public Animator animator;

    // set moving speed
    float movingspeed = 3.0f;

    // walking sound
    public AudioSource walkingAudio;

    void Update()
    {
        // get each inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // control movement when interaction is not processing
        if (isDoingInteract == 0)
        {
            // show vertical animation if have vertical input
            if (verticalInput != 0)
            {
                // set vertical value
                animator.SetFloat("Vertical", verticalInput);
                animator.SetFloat("Horizontal", 0);
            }

            // show horizontal animation if no vertical input
            else
            {
                // set horizontal input
                animator.SetFloat("Horizontal", horizontalInput);
                animator.SetFloat("Vertical", 0);
            }

            // make player move (horizontal)
            Vector3 horizontalMovement = new Vector3(horizontalInput, 0.0f, 0.0f);
            transform.position += movingspeed * horizontalMovement * Time.deltaTime;

            // make player move (vertical)
            Vector3 verticalMovement = new Vector3(0.0f, verticalInput, 0.0f);
            transform.position += movingspeed * verticalMovement * Time.deltaTime;

            // Check if player is moving
            if (horizontalInput != 0 || verticalInput != 0)
            {
                // Play walking sound if it's not already playing
                if (!walkingAudio.isPlaying)
                {
                    walkingAudio.loop = true; // Ensure it loops
                    walkingAudio.Play();
                }
            }
            else
            {
                // Stop walking sound if player stops moving
                if (walkingAudio.isPlaying)
                {
                    walkingAudio.Stop();
                }
            }
        }

        else
        {
            // Stop player animation and movement
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Horizontal", 0);

            // Stop walking sound if player is interacting
            if (walkingAudio.isPlaying)
            {
                walkingAudio.Stop();
            }
        }

    }
}
