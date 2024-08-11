using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 1.5f;
    public KeyCode interactionKey = KeyCode.E;
    
    // save the name of departureMap to choose the correct position of player
    public static string departureMap;


    [SerializeField] 
    private string sceneName;
    
    void Update()
    {
        // calculate distance between player and arrow
        float distance = Vector3.Distance(player.position, transform.position);

        // if close enough, change scene when press E
        if(distance <= interactionDistance )
        {
            //Debug.Log("Close Enough to Interact");
            if (Input.GetKeyDown(interactionKey))
            {
                departureMap = SceneManager.GetActiveScene().name;
                Debug.Log("departure map is : " + departureMap);
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
