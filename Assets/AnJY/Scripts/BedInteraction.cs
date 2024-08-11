using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BedInteraction : ActionPoints
{
    public Transform player;
    public float interactionDistance = 1.5f;
    public KeyCode interactionKey = KeyCode.E;

    void Update()
    {
        // calculate distance between player and arrow
        float distance = Vector3.Distance(player.position, transform.position);

        // if close enough, sleep when press E
        if (distance <= interactionDistance && Input.GetKeyDown(interactionKey))
        {
            sleep();
        }
    }
}