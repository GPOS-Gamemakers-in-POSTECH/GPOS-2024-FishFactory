// script for implementing fish tank

using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class FishTankManager : FishTankUIController
{
    public Transform player;

    // number of fish tank
    public FishTank fishTank;
    public int tankNumber;

    // interaction become active when player is closer to object than this value
    private float interactionDistance = 0.8f;

    // key to do interact
    private KeyCode interactionKey = KeyCode.E;

    // variable to indicate if fish tank is installed / istalled if value is 1
    private int isTankInstalled = 0;

    // popup to let player know when close enough to interact
    public GameObject installPopUp;
    public GameObject infoPopUp;

    // Tilemaps
    public Tilemap water;
    public Tilemap edge;
    public Tilemap indoorWater;


    // sound of installing fish tank
    public AudioSource installSound;
    
    // Start is called before the first frame update
    void Start()
    {
        fishTank = gameObject.AddComponent<FishTank>();
        isTankInstalled = searchTankInstallation()[tankNumber];

        if (isTankInstalled == 1)
        {
            edge.gameObject.SetActive(true);
            if (SceneManager.GetActiveScene().name == "Indoor" && indoorWater != null)
                indoorWater.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // calculate distance between player and tile
        Vector3 playerPosition = player.position;
        float distance = CalculateDistance(playerPosition, water);

        if (distance <= interactionDistance && isDoingInteract == 0)
        {            
            // if fish tank is not installed yet
            if (isTankInstalled == 0)
            { 
                // show pop up
                installPopUp.SetActive(true);

                // if interactionKey pressed, and condition satisfied, install fish tank
                // To Be Implemented
                // ( some condition that required to install the fishtank should be inserted in this if structure )
                if (Input.GetKeyDown(interactionKey) && isDoingInteract == 0)
                {
                    isDoingInteract = 1;
                    StartCoroutine(installFishTank());
                }
            }

            else
            {
                // show pop up
                infoPopUp.SetActive(true);

                // if interactionKey pressed, show information UI
                if (Input.GetKeyDown(interactionKey) && isDoingInteract == 0)
                {
                    isDoingInteract = 1;
                    activeFishTankUi(tankNumber);
                    infoPopUp.SetActive(false);
                }
            }
        }

        else if (distance <= interactionDistance && isDoingInteract == 1 && isTankInstalled == 1)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                isDoingInteract = 0;
                deactivateFishTankUi();
            }
        }

        else
        {
            installPopUp.SetActive(false);
            infoPopUp.SetActive(false);
        }
    }

    float CalculateDistance(Vector3 playerPosition, Tilemap tilemap)
    {
        // calculate boundary of tilemap
        TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
        Bounds tilemapBounds = tilemapRenderer.bounds;

        // found the closest point from player and calculate distance
        Vector3 closestPoint = tilemapBounds.ClosestPoint(player.position);
        return Vector3.Distance(playerPosition, closestPoint);
    }

    // coroutine to install fish tank
    IEnumerator installFishTank()
    {
        installPopUp.SetActive(false);

        // if installing in indoor, installing is seperated into two part
        if (SceneManager.GetActiveScene().name == "Indoor" && indoorWater != null)
        {
            // first activate tish tank edge
            installSound.Play();
            yield return new WaitForSeconds(installSound.clip.length / 2);
            edge.gameObject.SetActive(true);
            yield return new WaitForSeconds(installSound.clip.length / 2);
            
            // second activate water in it
            installSound.Play();
            yield return new WaitForSeconds(installSound.clip.length / 2);
            indoorWater.gameObject.SetActive(true);
            yield return new WaitForSeconds(installSound.clip.length / 2);
        }

        // else just activate the edge of fish tank
        else
        {
            installSound.Play();
            yield return new WaitForSeconds(installSound.clip.length / 2);
            edge.gameObject.SetActive(true);
            yield return new WaitForSeconds(installSound.clip.length / 2);
        }

        isTankInstalled = 1;
        searchTankInstallation()[tankNumber] = 1;   
        isDoingInteract = 0;
    }

    int[] searchTankInstallation()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MinMul") { return installStatusManager.Instance.MinMulFishTank; }
        else if (currentSceneName == "Ocean") { return installStatusManager.Instance.OceanFishTank; }
        else { return installStatusManager.Instance.IndoorFishTank; }
    }
}
