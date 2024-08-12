// script to control player movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovements : MonoBehaviour
{
    // aminator to control player animation
    public Animator animator;

    // set moving speed
    float movingspeed = 3.0f;

    void Update()
    {
        // get each inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

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

    }
}
