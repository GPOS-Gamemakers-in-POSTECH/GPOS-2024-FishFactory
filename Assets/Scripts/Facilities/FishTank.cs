using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FishTankManager : MonoBehaviour
{
    public int fishTankNo;
    public FishTankData fishTankData;

    public GameObject fishInfoUI; // UI for Fish Tank Information
    public GameObject infoPopup; // Information Pop-up for Fish Tank
    public GameObject installPopup; // Installation Pop-up for Fish Tank
    public Button fishButton; // Button for adding & gathering fishes from Fish Tank
    public Button feedButton; // Button for feeding fishes

    public GameObject player;
    private float interactionDistance = 0.8f; // Max Distance to Interact with Fish Tank
    private KeyCode interactionKey = KeyCode.E; // KeyCode for Interact

    // TileBase & Tilemaps
    public TileBase tileBaseA;
    public TileBase tileBaseB;
    public Tilemap baseTile;
    public Tilemap edgeTile;

    public AudioSource installSound; // Sound for Installation

    public UIController controlUI;


    // Start is called before the first frame update
    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "MinMul") { fishTankData = GameManager.Instance.freshFishTanks[fishTankNo]; }
        else if (SceneManager.GetActiveScene().name == "Ocean") { fishTankData = GameManager.Instance.oceanFishTanks[fishTankNo]; }
        else if (SceneManager.GetActiveScene().name == "Indoor") { fishTankData = GameManager.Instance.indoorFishTanks[fishTankNo]; }
        else { Debug.Log("This Scene has no Fish Tank"); }

        controlUI = UIController.Instance;
        player = GameObject.FindWithTag("Player"); // Find Player Object

        fishButton.onClick.AddListener(() => AddFish(GameManager.Instance.itemDict[0][10100], 1));
        feedButton.onClick.AddListener(FeedFish);

        if (fishTankData.isTankInstalled)
        {
            edgeTile.gameObject.SetActive(true);
            if (SceneManager.GetActiveScene().name == "Indoor") { baseTile.SwapTile(tileBaseA, tileBaseB); }
        }
    }

    // Update is called once per frame
    public void Update()
    {
        float distance = CalculateDistance(player, baseTile);

        if (distance <= interactionDistance && GameManager.Instance.isInteracting == false)
        {
            // if fish tank is not installed yet
            if (!fishTankData.isTankInstalled)
            {
                // show pop up
                installPopup.SetActive(true);

                // if interactionKey pressed, and condition satisfied, install fish tank
                if (Input.GetKeyDown(interactionKey) && !GameManager.Instance.isInteracting)
                {
                    if (controlUI.ReduceActionPoints(15))
                    {
                        GameManager.Instance.isInteracting = true;
                        StartCoroutine(InstallFishTank());
                    }                    
                }
            }

            else
            {
                // show pop up
                infoPopup.SetActive(true);

                // if interactionKey pressed, show information UI
                if (Input.GetKeyDown(interactionKey) && !GameManager.Instance.isInteracting)
                {
                    ShowFishTankInfo();
                }
            }
        }

        else if (distance <= interactionDistance && GameManager.Instance.isInteracting && fishTankData.isTankInstalled)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                GameManager.Instance.isInteracting = false;
                fishInfoUI.SetActive(false);
            }
        }

        else
        {
            installPopup.SetActive(false);
            infoPopup.SetActive(false);
        }
    }

    // Install FIsh Tank
    IEnumerator InstallFishTank()
    {
        installPopup.SetActive(false);

        // Activate Edge Tiles
        installSound.Play();
        yield return new WaitForSeconds(installSound.clip.length / 2);
        edgeTile.gameObject.SetActive(true);
        yield return new WaitForSeconds(installSound.clip.length / 2);

        // Swap Base Tiles to Water
        if (SceneManager.GetActiveScene().name == "Indoor")
        {
            installSound.Play();
            yield return new WaitForSeconds(installSound.clip.length / 2);
            baseTile.SwapTile(tileBaseA, tileBaseB);
            yield return new WaitForSeconds(installSound.clip.length / 2);
        }

        fishTankData.isTankInstalled = true;
        GameManager.Instance.isInteracting = false;
    }

    // Show the Information of Fish Tank
    public void ShowFishTankInfo()
    {
        GameManager.Instance.isInteracting = true;
        fishInfoUI.SetActive(true);
        infoPopup.SetActive(false);
    }

    // Add New Fishes to Fish Tank
    public void AddFish(Item fish, int fishAmount)
    {
        if (fishTankData.fish == null)
        {
            fishTankData.fish = fish;
            fishTankData.fishAmount = fishAmount;
        }
        else { Debug.Log("Fish Already Exists."); }

        return;
    }

    // Gather Grown Fishes
    public void GatherFish()
    {
        if (fishTankData.growCount >= fishTankData.fish.time) { Debug.Log("Gathered"); }
        else if (fishTankData.dieCount >= 3) { Debug.Log("Died"); }
        else { Debug.Log("Not Yet"); }
    }

    // Add New Parts to Fish Tank
    public void AddParts(Item newParts)
    {
        for (int i = 0; i < 4; i++)
        {
            if (fishTankData.parts[i].itemID / 1000 == newParts.itemID / 1000)
            {
                if (fishTankData.parts[i].itemID == newParts.itemID) { Debug.Log("Parts Already Exists"); }
                else if (fishTankData.parts[i].itemID < newParts.itemID)
                {
                    fishTankData.parts[i] = newParts;
                    Debug.Log("Changed");
                }
                else { Debug.Log("Low Tier"); }

                return;
                    
            }
            else { Debug.Log("Invalid Item"); }
        }

        return;
    }

    // Check Parts of Fish Tank
    public void CheckPartsOn()
    {
        for (int i = 0; i < 4; i++)
        {
            if (fishTankData.parts[i].itemID != 0) { fishTankData.isPartsOn[i] = true; }
            else { fishTankData.isPartsOn[i] = false; }
        }

        return;
    }

    // Feed Fishes
    public void FeedFish()
    {
        if (fishTankData.isPartsOn[3] == true)
        {
            fishTankData.parts[3].feedAmount -= fishTankData.fish.feedAmount * fishTankData.fishAmount;
        }

        Debug.Log("Feeded");
    }

    // Check fish's grown days or days to die
    public void DecideFishHealth()
    {
        if (fishTankData.isPartsOn[0] == false && fishTankData.isPartsOn[1] == false) { fishTankData.dieCount += 1; }
        else { fishTankData.dieCount = 0; fishTankData.growCount += 1; }

        if (fishTankData.dieCount == 3) { }

        return;
    }

    // Calculate Distance from Player to Fish Tank
    public float CalculateDistance(GameObject player, Tilemap tilemap)
    {
        // Calculate boundary of Tilemap
        TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
        Bounds tilemapBounds = tilemapRenderer.bounds;

        // Find the closest point and Calculate Distance
        Vector3 closestPoint = tilemapBounds.ClosestPoint(player.transform.position);
        return Vector3.Distance(player.transform.position, closestPoint);
    }
}

[Serializable]
public class FishTankData
{
    public int fishTankNo; // Number of Fish Tank
    public int waterType; // Water Type of Fish Tank
    public bool isTankInstalled; // Check if Fish Tank is Installed

    public Item fish; // Fish Item
    public int fishAmount; // Amount of Fishes
    public int dieCount; // Die Count of Fishes
    public int growCount; // Grow Count of Fishes

    public Item[] parts = new Item[4]; // Parts on Fish Tank
    public bool[] isPartsOn = new bool[4]; // Bool variable for which parts is on
}