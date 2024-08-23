// Script to implement interaction that pops up UI

using System.Collections;
using UnityEngine;

public class popupUIinteraction : MonoBehaviour
{
    public Transform player;

    public float interactionDistance = 1.5f; // Interaction become active when player is closer than this value
    public KeyCode interactionKey = KeyCode.E; // Key to interact

    // UI that pops up when interaction occurs
    public GameObject popupUI;

    // popup to let player know when close enough to interact
    public GameObject interactionPopUp;

    // value - 0 : mining, 1 - selling
    public int objectKind;

    public AudioSource soundEffect;

    // Update is called once per frame
    void Update()
    {
        // if close enough, start process of interaction        
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= interactionDistance)
        {
            // activate popup
            interactionPopUp.SetActive(true);

            // when interactionkey pressed, and interaction is not processing
            if (Input.GetKeyDown(interactionKey) && GameManager.Instance.isInteracting == false)
            {
                GameManager.Instance.isInteracting = true;
                if (objectKind == 0) { StartCoroutine(Mining()); } // Mining
                else { popupUI.SetActive(true); } // Trading
            }
        }
        else { interactionPopUp.SetActive(false); }        
    }

    // if mining arrow interacted, activate mining UI during the length of mining sound
    IEnumerator Mining()
    {
        popupUI.SetActive(true);
        if (soundEffect != null)
        {
            soundEffect.Play();
            yield return new WaitForSeconds(soundEffect.clip.length);
            soundEffect.Play();
            yield return new WaitForSeconds(soundEffect.clip.length);
        }

        popupUI.SetActive(false);
        GameManager.Instance.isInteracting = false;
    }
}
