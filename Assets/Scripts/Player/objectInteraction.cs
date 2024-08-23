// Script for implement Object Interaction which switches Scene

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ObjectInteraction : fadeInOut
{
    public Transform player;

    public float interactionDistance = 1.0f; // Interaction become active when player is closer than this value
    public KeyCode interactionKey = KeyCode.E; // Key to interact

    // value - 0 : arrow that moves to new map, 1 : bed
    public int objectKind;

    // save the name of departureMap to choose the correct position of player
    public static string departureMap;

    [SerializeField] 
    private string sceneName; // select scene to move into

    // popup to let player know when close enough to interact
    public GameObject interactionPopUp;

    // sound effect of transition
    public AudioSource transitionAudio;

    public int totalDate;
    public int actionPoint;
    int maxActionPoint = 100;
    
    void Start()
    {
        if (departureMap == sceneName) { player.position = transform.position; } // set players position to right arrow that is connected to departured map
        StartCoroutine(FadeFunction(1f)); // fade in when scene is started

        actionPoint = GameManager.Instance.actionPoint;
        totalDate = GameManager.Instance.totalDate;
    }

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
            if (Input.GetKeyDown(interactionKey) && GameManager.Instance.isInteracting == false)
            {

                
                // save the departureMap name
                departureMap = SceneManager.GetActiveScene().name;                

                // if object is arrow, fade and move to connected scene
                if (objectKind == 0)
                {
                    StartCoroutine(FadeAndLoadScene(sceneName, transitionAudio));
                }

                // if object is bed, move to sleep scene
                else if (objectKind == 1)
                {
                    StartCoroutine(Sleep());
                }
            }
        }

        // if player moves away, deactivate popup
        else
        {
            interactionPopUp.SetActive(false);
        }
    }

    IEnumerator Sleep()
    {
        // fill the AP to max and add date
        GameManager.Instance.isInteracting = true;
        GameManager.Instance.actionPoint = maxActionPoint;
        totalDate++;

        // fade out and move to SleepScene
        StartCoroutine(FadeFunction(0f));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SleepScene");
    }
}
