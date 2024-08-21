using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class facilityManager : ActionPoints
{
    public Transform player;

    public int lineNumber;

    private float interactionDistance = 0.8f;

    private KeyCode interactionKey = KeyCode.E;
    private KeyCode destoryKey = KeyCode.Q;

    private int[] lineStatus;
    private int[] elementStatus;
    private int[] isWorking;

    public GameObject installPopUp;
    public GameObject inputPopUp;

    public Tilemap[] elementTiles = new Tilemap[4];
    private int currentTile;

    public AudioSource installSound;

    List<int> ableElements;

    // Start is called before the first frame update
    void Start()
    {
        lineStatus = installStatusManager.Instance.facilityLine;
        elementStatus = installStatusManager.Instance.facilityElements[lineNumber];
        isWorking = installStatusManager.Instance.isFacilityWorking;
        currentTile = setCurrentTile();
        if (isWorking[lineNumber] == 1)
        {
            Debug.Log(lineNumber + "번 작동 종료");
            isWorking[lineNumber] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        Vector3 playerPosition = player.position;
        float distance = CalculateDistance(playerPosition, elementTiles[currentTile]);

        if (distance <= interactionDistance)
        {
            if (lineStatus[lineNumber] != 0)
            {
                if (isWorking[lineNumber]==0)
                {
                    if(Input.GetKeyDown(interactionKey))
                    {
                        Debug.Log("물고기 투입");
                        isWorking[lineNumber] = 1;
                    }
                }
            }

            else
            {
                if (Input.GetKeyDown(interactionKey) && isDoingInteract == 0)
                {
                    ableElements = GetOutput(currentTile == 0 ? 0 : elementStatus[currentTile - 1]);
                    Debug.Log(string.Join(", ", ableElements));
                    isDoingInteract = 1;
        
                }

                else if(isDoingInteract == 1)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Debug.Log(currentTile + "번 위치에" + ableElements[0] + "번 설치");
                        elementStatus[currentTile] = ableElements[0];
                        isDoingInteract = 0;
                        if (ableElements[0] == 4)
                        {
                            lineStatus[lineNumber] = checkLineType();
                            Debug.Log(lineStatus[lineNumber]+"번 라인 완성");
                        }
                        currentTile = setCurrentTile();
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Debug.Log(currentTile + "번 위치에" + ableElements[1] + "번 설치");
                        elementStatus[currentTile] = ableElements[1];
                        isDoingInteract = 0;
                        currentTile = setCurrentTile();
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Debug.Log(currentTile + "번 위치에" + ableElements[2] + "번 설치");
                        elementStatus[currentTile] = ableElements[2];
                        isDoingInteract = 0;
                        currentTile = setCurrentTile();
                    }
                }
            }
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
