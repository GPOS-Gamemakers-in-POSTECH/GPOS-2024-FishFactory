using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FishTank : MonoBehaviour
{
    public int fishTankNo; // Number of Fish Tank
    public int waterType; // Water Type of Fish Tank

    public Item fish; // Fish Item
    public int fishAmount; // Amount of Fishes
    public int dieCount; // Die Count of Fishes
    public int growCount; // Grow Count of Fishes

    public Item[] parts; // Parts on Fish Tank
    public bool[] isPartsOn; // Bool variable for which parts is on

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

    public bool isTankInstalled; // Check if Fish Tank is Installed
    public AudioSource installSound; // Sound for Installation



    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindWithTag("Player"); // Find Player Object

        fishButton.onClick.AddListener(() => AddFish(null, 0));
        feedButton.onClick.AddListener(FeedFish);

        isTankInstalled = SearchTankInstallation()[fishTankNo];

        if (isTankInstalled)
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
            if (!isTankInstalled)
            {
                // show pop up
                installPopup.SetActive(true);

                // if interactionKey pressed, and condition satisfied, install fish tank
                if (Input.GetKeyDown(interactionKey) && !GameManager.Instance.isInteracting)
                {
                    GameManager.Instance.isInteracting = true;
                    StartCoroutine(InstallFishTank());
                }
            }

            else
            {
                // show pop up
                infoPopup.SetActive(true);

                // if interactionKey pressed, show information UI
                if (Input.GetKeyDown(interactionKey) && !GameManager.Instance.isInteracting)
                {
                    GameManager.Instance.isInteracting = true;
                    fishInfoUI.SetActive(true);
                    infoPopup.SetActive(false);
                }
            }
        }

        else if (distance <= interactionDistance && GameManager.Instance.isInteracting && isTankInstalled)
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


    // Constructor
    public FishTank()
    {
        fish = null;
        fishAmount = 0;
        parts = new Item[] { null, null, null, null };
        isPartsOn = new bool[] { false, false, false, false };
        dieCount = 0;
        growCount = 0;
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

        isTankInstalled = true;
        SearchTankInstallation()[fishTankNo] = true;
        GameManager.Instance.isInteracting = false;
    }

    // Show the Information of Fish Tank
    public void ShowFishTankInfo()
    {
        Debug.Log(fish.itemID);
        Debug.Log(fish.itemName);
        Debug.Log(fishAmount);
        Debug.Log(isPartsOn);
    }

    // Add New Fishes to Fish Tank
    public void AddFish(Item fish, int fishAmount)
    {
        if (this.fish == null)
        {
            this.fish = fish;
            this.fishAmount = fishAmount;
        }
        else { Debug.Log("Fish Already Exists."); }

        return;
    }

    // Gather Grown Fishes
    public void GatherFish()
    {
        if (growCount >= fish.growTime) { Debug.Log("Gathered"); }
        else if (dieCount >= 3) { Debug.Log("Died"); }
        else { Debug.Log("Not Yet"); }
    }

    // Add New Parts to Fish Tank
    public void AddParts(Item newParts)
    {
        for (int i = 0; i < 4; i++)
        {
            if (parts[i].itemID / 1000 == newParts.itemID / 1000)
            {
                if (parts[i].itemID == newParts.itemID) { Debug.Log("Parts Already Exists"); }
                else if (parts[i].itemID < newParts.itemID)
                {
                    parts[i] = newParts;
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
            if (parts[i].itemID != 0) { isPartsOn[i] = true; }
            else { isPartsOn[i] = false; }
        }

        return;
    }

    // Feed Fishes
    public void FeedFish()
    {
        if (isPartsOn[3] == true)
        {
            parts[3].feedAmount -= fish.feedAmount * fishAmount;
        }

        Debug.Log("Feeded");
    }

    // Check fish's grown days or days to die
    public void DecideFishHealth()
    {
        if (isPartsOn[0] == false && isPartsOn[1] == false) { dieCount += 1; }
        else { dieCount = 0; growCount += 1; }

        if (dieCount == 3) { }

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

    bool[] SearchTankInstallation()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MinMul") { return GameManager.Instance.freshFishTank; }
        else if (currentSceneName == "Ocean") { return GameManager.Instance.oceanFishTank; }
        else { return GameManager.Instance.indoorFishTank; }
    }
}
