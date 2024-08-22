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

    List<int> ableElements;  // to save the kinds of installable facility

    void Start()
    {
        // load status datas from installStatusManager
        lineStatus = installStatusManager.Instance.facilityLine;
        elementStatus = installStatusManager.Instance.facilityElements[lineNumber];
        isWorking = installStatusManager.Instance.isFacilityWorking;

        for (int i = 0; i < 4; i++)
        {
            elementTileColliders[i] = elementTiles[i].GetComponent<TilemapCollider2D>();
            if (elementStatus[i] != 0)
            {
                elementTiles[i].SwapTile(groundTile, facilityTile);
                elementTileColliders[i].enabled = true;
            }
        }

        if (elementStatus[0] == 0)
            elementTiles[0].SwapTile(groundTile, interactTile);

        currentTile = setCurrentTile();
        if (isWorking[lineNumber] == 1)
        {
            Debug.Log(lineNumber + "번 작동 종료");
            isWorking[lineNumber] = 0;
        }
    }
    
    void Update()
    {        
        Vector3 playerPosition = player.position;
        float currentTileDistance = CalculateDistance(playerPosition, elementTiles[currentTile]);
        float mainTileDistance = CalculateDistance(playerPosition, elementTiles[0]);

        // if player is close enough to main tile, activate destory option
        if(mainTileDistance <= interactionDistance)
        {
            if (elementStatus[0] != 0)  // when the line has at least one element
            {
                if (Input.GetKeyDown(destroyKey))
                {
                    Debug.Log("Line destroyed!");
                    lineStatus[lineNumber] = 0;
                    for (int i = 0; i < 4; i++)
                        elementStatus[i] = 0;
                    currentTile = setCurrentTile();
                }
            }
        }

        // if close enough, start interacting process
        if (currentTileDistance <= interactionDistance)
        {
            // if line is completed, player can input fish
            if (lineStatus[lineNumber] != 0)
            {                
                if (isWorking[lineNumber]==0)
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

            else
            {
                if (Input.GetKeyDown(interactionKey) && GameManager.Instance.isInteracting == false)
                {
                    ableElements = GetOutput(currentTile == 0 ? 0 : elementStatus[currentTile - 1]);
                    Debug.Log(string.Join(", ", ableElements));
                    GameManager.Instance.isInteracting = true;
        
                }

                else if(GameManager.Instance.isInteracting == true)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Debug.Log(currentTile + "번 위치에" + ableElements[0] + "번 설치");
                        elementStatus[currentTile] = ableElements[0];
                        elementTiles[currentTile].SwapTile(interactTile, facilityTile);
                        installSound.Play();
                        GameManager.Instance.isInteracting = false;
                        if (ableElements[0] == 4)
                        {
                            lineStatus[lineNumber] = checkLineType();
                            Debug.Log(lineStatus[lineNumber]+"번 라인 완성");
                        }
                        currentTile = setCurrentTile();
                        elementTiles[currentTile].SwapTile(groundTile, interactTile);
                        elementTileColliders[currentTile].enabled = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Debug.Log(currentTile + "번 위치에" + ableElements[1] + "번 설치");
                        elementStatus[currentTile] = ableElements[1];
                        GameManager.Instance.isInteracting = false;
                        currentTile = setCurrentTile();
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Debug.Log(currentTile + "번 위치에" + ableElements[2] + "번 설치");
                        elementStatus[currentTile] = ableElements[2];
                        GameManager.Instance.isInteracting = false;
                        currentTile = setCurrentTile();
                    }
                }
            }
        }

        else
        {
            inputPopUp.SetActive(false);
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

    List<int> GetOutput(int input)
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
        // 배열을 변수로 선언
        int[] status1 = new int[] { 1, 4, 0, 0 };
        int[] status2 = new int[] { 2, 4, 0, 0 };
        int[] status3 = new int[] { 3, 5, 4, 0 };
        int[] status4 = new int[] { 3, 6, 8, 4 };
        int[] status5 = new int[] { 3, 7, 4, 0 };

        if (compareArr(elementStatus, status1)) return 1; // 냉동
        else if (compareArr(elementStatus, status2)) return 2; // 말리기
        else if (compareArr(elementStatus, status3)) return 3; // 젓갈
        else if (compareArr(elementStatus, status4)) return 4; // 어묵
        else /*if (compareArr(elementStatus, status5))*/ return 5; // 통조림     
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
}
