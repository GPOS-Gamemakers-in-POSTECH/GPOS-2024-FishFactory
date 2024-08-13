// script for implement interaction to objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class objectInteraction : ActionPoints
{
    public Transform player;

    // interaction become active when player is closer to object than this value
    public float interactionDistance = 1.5f;

    // key to do interact
    public KeyCode interactionKey = KeyCode.E;

    // value - 0 : object is arrow, 1 : object is bed
    public int isBed;

    // save the name of departureMap to choose the correct position of player
    public static string departureMap;

    [SerializeField] 
    private string sceneName; // select scene to move into

    // popup to let player know when close enough to interact
    public GameObject interactionPopUp;

    // sound effect of transition
    public AudioSource transitionAudio;
    
    void Start()
    {
        // set players position to right arrow that is connected to departured map
        if (departureMap == sceneName)
        {
            player.position = transform.position;
        }
        // fade in when scene is started
        StartCoroutine(FadeFunction(1f));

        isDoingInteract = 0;
    }

    void Update()
    {
        // calculate distance between player and arrow
        float distance = Vector3.Distance(player.position, transform.position);

        // if close enough, start process of interaction
        if (distance <= interactionDistance )
        {
            // activate popup
            interactionPopUp.SetActive(true);
            
            // when interactionkey pressed, and interaction is not processing
            if (Input.GetKeyDown(interactionKey) && isDoingInteract == 0 )
            {
                isDoingInteract = 1;
                
                // save the departureMap name
                departureMap = SceneManager.GetActiveScene().name;                

                // if object is arrow, fade and move to connected scene
                if (isBed == 0)
                {
                    StartCoroutine(FadeAndLoadScene(sceneName, transitionAudio));
                }

                // if object is bed, move to sleep scene
                else
                {
                    StartCoroutine(sleep());
                }
            }
        }

        // if player moves away, deactivate popup
        else
        {
            interactionPopUp.SetActive(false);
        }
    }
}
