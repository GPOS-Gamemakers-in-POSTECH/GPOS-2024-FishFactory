using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArrowInteraction : fadeInOut
{
    public Transform player;
    public float interactionDistance = 1.5f;
    public KeyCode interactionKey = KeyCode.E;
    
    // save the name of departureMap to choose the correct position of player
    public static string departureMap;

    [SerializeField] 
    private string sceneName; // select scene to move into

    public GameObject arrowPopUp;
    
    void Start()
    {
        // fade in
        StartCoroutine(FadeFunction(1f));
    }

    void Update()
    {
        // calculate distance between player and arrow
        float distance = Vector3.Distance(player.position, transform.position);

        // if close enough, change scene when press E
        if(distance <= interactionDistance )
        {
            arrowPopUp.SetActive(true);
            //Debug.Log("Close Enough to Interact");
            if (Input.GetKeyDown(interactionKey))
            {
                // save the departureMap name
                departureMap = SceneManager.GetActiveScene().name;
                Debug.Log("departure map is : " + departureMap);

                StartCoroutine(FadeAndLoadScene(sceneName));
            }
        }
        else
        {
            arrowPopUp.SetActive(false);
        }
    }
}
