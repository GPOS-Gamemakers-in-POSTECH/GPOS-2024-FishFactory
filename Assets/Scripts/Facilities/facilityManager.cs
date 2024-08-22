using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class facilityManager : MonoBehaviour
{
    public Transform player;
    private float interactionDistance = 0.8f;

    private KeyCode interactionKey = KeyCode.E;
    private KeyCode destroyKey = KeyCode.Q;
    
    public int lineNumber;  // index of this line

    public Item recipe;

    private int[] lineStatus;  // kind of line
    private int[] elementStatus;  // kind of elements
    private int[] isWorking;  // is this line working

    public GameObject installPopUp;
    public GameObject inputPopUp;

    public Tilemap[] elementTiles = new Tilemap[4];  // four element tiles of this line
    private TilemapCollider2D[] elementTileColliders = new TilemapCollider2D[4];
    private int currentTile;  // which tile is now on interact

    public TileBase groundTile;
    public TileBase interactTile;
    public TileBase facilityTile;

    public AudioSource installSound;

    public JsonManager jsonManager;

    List<int> ableElements;  // to save the kinds of installable facility

    void Awake()
    {
        for (int i=0; i<4; i++)
            elementTileColliders[i] = elementTiles[i].GetComponent<TilemapCollider2D>();
    }

    void Start()
    {
        // load status datas from installStatusManager
        lineStatus = installStatusManager.Instance.facilityLine;
        elementStatus = installStatusManager.Instance.facilityElements[lineNumber];
        isWorking = installStatusManager.Instance.isFacilityWorking;

        for (int i = 0; i < 4; i++)
        {
            if (elementStatus[i] != 0)
            {
                elementTiles[i].SwapTile(groundTile, facilityTile);
                elementTileColliders[i].enabled = true;
                if (elementStatus[i] == 4)
                    break;
            }
            else
            {
                elementTiles[i].SwapTile(groundTile, interactTile);
                elementTileColliders[i].enabled = true;
                break;
            }
        }

        if (elementStatus[0] == 0)
            elementTiles[0].SwapTile(groundTile, interactTile);

        currentTile = setCurrentTile();

        if (isWorking[lineNumber] == 2)
        {
            Debug.Log("working is ended in no." + lineNumber);
            isWorking[lineNumber] = 0;
        }
    }
    
    void Update()
    {        
        Vector3 playerPosition = player.position;
        float currentTileDistance = CalculateDistance(playerPosition, elementTiles[currentTile]);
        float mainTileDistance = CalculateDistance(playerPosition, elementTiles[0]);

        if(mainTileDistance <= interactionDistance)
        {
            // if player is close enough to main tile and line has at least one element, activate destory option
            if (elementStatus[0] != 0)
            {
                if (Input.GetKeyDown(destroyKey))
                {
                    Debug.Log("Line destroyed!");
                    lineStatus[lineNumber] = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        elementStatus[i] = 0;
                        elementTiles[i].SwapTile(facilityTile, groundTile);
                        elementTiles[i].SwapTile(interactTile, groundTile);
                        elementTileColliders[i].enabled = false;
                    }
                    elementStatus[0] = 0;
                    elementTiles[0].SwapTile(facilityTile, interactTile);
                    isWorking[lineNumber] = 0;

                    currentTile = setCurrentTile();
                }
            }
            // if line is completed, player can input fish
            if (lineStatus[lineNumber] != 0)
            {
                if (isWorking[lineNumber] == 0)
                {
                    inputPopUp.SetActive(true);
                    if (Input.GetKeyDown(interactionKey))
                    {
                        Debug.Log("물고기 투입");
                        isWorking[lineNumber] = 1;
                        inputPopUp.SetActive(false);
                    }
                }
            }
        }

        // if close enough, start interacting process
        if (currentTileDistance <= interactionDistance && lineStatus[lineNumber] == 0)
        {
            inputPopUp.SetActive(false);

            if (Input.GetKeyDown(interactionKey) && GameManager.Instance.isInteracting == false)
            {
                ableElements = getAbleFacilityList(currentTile == 0 ? 0 : elementStatus[currentTile - 1]);
                Debug.Log(string.Join(", ", ableElements));
                GameManager.Instance.isInteracting = true;
        
            }

            else if(GameManager.Instance.isInteracting == true)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    StartCoroutine(installFacility(0));
                }
                else if (ableElements.Count==1)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
                        Debug.Log("Invalid Input");
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    StartCoroutine(installFacility(1));
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    StartCoroutine(installFacility(2));
                }
                else if (Input.GetKeyDown(interactionKey))
                    GameManager.Instance.isInteracting = false;
            }            
        }

        else
        {
            installPopUp.SetActive(false);
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

    int setCurrentTile()
    {
        if (lineStatus[lineNumber] != 0)
            return 0;

        else if (elementStatus[0] == 0)
            return 0;

        else if (elementStatus[1] == 0)
            return 1;

        else if (elementStatus[2] == 0)
            return 2;

        else // if (elementStatus[3] == 0)
            return 3;
    }

    List<int> getAbleFacilityList(int input)
    {
        List<int> result = new List<int>();

        switch (input)
        {
            case 1:
            case 2:
            case 5:
            case 7:
            case 8:
                result.Add(4);
                break;
            case 3:
                result.Add(5);
                result.Add(6);
                result.Add(7);
                break;
            case 0:
                result.Add(1);
                result.Add(2);
                result.Add(3);
                break;
            case 4:
                result.Add(0);
                break;
            case 6:
                result.Add(8);
                break;
        }

        return result;
    }

    int checkLineType()
    {
        int[] status1 = new int[] { 1, 4, 0, 0 };
        int[] status2 = new int[] { 2, 4, 0, 0 };
        int[] status3 = new int[] { 3, 5, 4, 0 };
        int[] status4 = new int[] { 3, 6, 8, 4 };
        int[] status5 = new int[] { 3, 7, 4, 0 };

        if (compareArr(elementStatus, status1)) return 1; // freeze
        else if (compareArr(elementStatus, status2)) return 2; // dry
        else if (compareArr(elementStatus, status3)) return 3; // jutgal
        else if (compareArr(elementStatus, status4)) return 4; // fishcake
        else /*if (compareArr(elementStatus, status5))*/ return 5; // can   
    }

    bool compareArr(int[] arr1, int[] arr2)
    {    
        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
                return false;
        }

        return true;
    }

    IEnumerator installFacility(int elementIndex)
    {
        GameManager.Instance.isInteracting = true;
        Debug.Log(currentTile + "번 위치에" + ableElements[elementIndex] + "번 설치");
        installSound.Play();
        elementStatus[currentTile] = ableElements[elementIndex];
        elementTiles[currentTile].SwapTile(interactTile, facilityTile);
        
        GameManager.Instance.isInteracting = false;
        if (ableElements[0] == 4)
        {
            lineStatus[lineNumber] = checkLineType();
            Debug.Log(lineStatus[lineNumber] + "번 라인 완성");
        }
        yield return new WaitForSeconds(installSound.clip.length / 2);
        currentTile = setCurrentTile();
        elementTiles[currentTile].SwapTile(groundTile, interactTile);
        elementTileColliders[currentTile].enabled = true;
        GameManager.Instance.isInteracting = false;
    }

    /*
    Item makeProduct(int fishID, int lineKind)
    {
        if(lineKind == 1) // freeze
        {
            switch(fishID)
                case :
        }
        else if(lineKind == 2) // dry
        {

        }
        else if(lineKind == 3) // jeotgal
        {
            
        }
        else if(lineKind == 4) // fish cake
        {

        }
        else // if(lineKind == 5) // can
        {
            
        }
    }*/
}
