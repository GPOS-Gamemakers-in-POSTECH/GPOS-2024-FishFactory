// script to implement interaction that pops up some UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popupUIinteraction : ActionPoints
{
    public Transform player;

    // interaction become active when player is closer to object than this value
    public float interactionDistance = 1.5f;

    // key to do interact
    public KeyCode interactionKey = KeyCode.E;

    // UI that pops up when interaction occurs
    public GameObject popupUI;

    // popup to let player know when close enough to interact
    public GameObject interactionPopUp;

    // value - 0 : mining, 1 - selling
    public int objectKind;

    public AudioSource miningAudio;

    // Update is called once per frame
    void Update()
    {
        // calculate distance between player and arrow
        float distance = Vector3.Distance(player.position, transform.position);

        // if close enough, start process of interaction
        if (distance <= interactionDistance)
        {
            // activate popup
            interactionPopUp.SetActive(true);

            // when interactionkey pressed, and interaction is not processing
            if (Input.GetKeyDown(interactionKey) && isDoingInteract == 0)
            {
                isDoingInteract = 1;

                // if mining
                if(objectKind == 0)
                    StartCoroutine(mining());

                // if selling
                else
                    popupUI.SetActive(true);
            }
        }

        // if player moves away, deactivate popup
        else
        {
            interactionPopUp.SetActive(false);
        }
    }

    // if mining arrow interacted, activate mining UI for length of mining sound
    IEnumerator mining()
    {
        popupUI.SetActive(true);
        if (miningAudio != null)
        {
            miningAudio.Play();
            yield return new WaitForSeconds(miningAudio.clip.length);
            miningAudio.Play();
            yield return new WaitForSeconds(miningAudio.clip.length);
        }

        popupUI.SetActive(false);
        isDoingInteract = 0;
    }
}
