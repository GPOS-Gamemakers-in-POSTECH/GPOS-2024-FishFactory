using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

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
    private int[] inputCount;  // amount of fishes input

    private int inputFish = 0;

    public GameObject installPopUp;
    public GameObject inputPopUp;

    public Tilemap[] elementTiles = new Tilemap[4];  // four element tiles of this line
    private TilemapCollider2D[] elementTileColliders = new TilemapCollider2D[4];
    private int currentTile;  // which tile is now on interact

    public Tilemap[] exampleTiles = new Tilemap[10];

    public GameObject inputPopUpText;
    public GameObject installPopUpText;
    string allFishNames;


    public AudioSource installSound;

    public UIController controlUI;

    List<int> ableElements;  // to save the kinds of installable facilities
    List<int> ableFishes; // to save the kinds of inputable fishes

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
        inputCount = installStatusManager.Instance.facilityInputCount;

        controlUI = UIController.Instance;

        for (int i = 0; i < 4; i++)
        {
            if (elementStatus[i] != 0)
            {
                CopyTiles(elementTiles[i], elementStatus[i]);
                elementTileColliders[i].enabled = true;
                if (elementStatus[i] == 4)
                    break;
            }
            else
            {                
                CopyTiles(elementTiles[i], 9);
                elementTileColliders[i].enabled = true;
                break;
            }
        }

        if (elementStatus[0] == 0)
            CopyTiles(elementTiles[0], 9);


        currentTile = setCurrentTile();

        /*if (isWorking[lineNumber] % 100 != 1 && isWorking[lineNumber] != 0)
        {
            int productID = isWorking[lineNumber] + 1 + lineStatus[lineNumber];
            inputPopUpText.GetComponent<TextMeshPro>().text = GameManager.Instance.itemDict[0][productID].itemName + (inputCount[lineNumber] / 2).ToString() + "�� ���� ����";
            Debug.Log("product made : " + GameManager.Instance.itemDict[0][productID].itemName + inputCount[lineNumber] / 2 + "�� ���� ����");
            isWorking[lineNumber] = 0;
            inputCount[lineNumber] = 0;
        }*/
    }   


    void Update()
    {        
        Vector3 playerPosition = player.position;
        float currentTileDistance = CalculateDistance(playerPosition, elementTiles[currentTile]);
        float mainTileDistance = CalculateDistance(playerPosition, elementTiles[0]);

        if (isWorking[lineNumber] % 100 != 1 && isWorking[lineNumber] != 0)
        {
            int productID = isWorking[lineNumber] + 1 + lineStatus[lineNumber];
            inputPopUp.SetActive(true);
            inputPopUpText.GetComponent<TextMeshPro>().text = GameManager.Instance.itemDict[0][productID].itemName + (inputCount[lineNumber] / 2).ToString() + "�� ���� ����\n E : ����";
            Debug.Log("product made : " + GameManager.Instance.itemDict[0][productID].itemName + inputCount[lineNumber] / 2 + "�� ���� ����");

            if (Input.GetKeyDown(interactionKey))
            {
                inputPopUp.SetActive(false);
                isWorking[lineNumber] = 0;
                inputCount[lineNumber] = 0;
            }
        }

        else
        {
            if (mainTileDistance <= interactionDistance)
            {
                // if player is close enough to main tile and line has at least one element, activate destory option
                if (elementStatus[0] != 0 && GameManager.Instance.isInteracting == false)
                {
                    if (Input.GetKeyDown(destroyKey))
                    {
                        lineStatus[lineNumber] = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            elementStatus[i] = 0;
                            CopyTiles(elementTiles[i], 0);
                            elementTileColliders[i].enabled = false;
                        }
                        elementStatus[0] = 0;
                        CopyTiles(elementTiles[0], 9);
                        isWorking[lineNumber] = 0;
                        inputCount[lineNumber] = 0;

                        currentTile = setCurrentTile();
                    }
                }
                // if line is completed, player can input fish
                if (lineStatus[lineNumber] != 0)
                {
                    if (isWorking[lineNumber] == 0)
                    {
                        if (GameManager.Instance.isInteracting == false)
                        {
                            if (Input.GetKeyDown(interactionKey))
                            {
                                inputPopUp.SetActive(true);
                                GameManager.Instance.isInteracting = true;
                                ableFishes = getAbleFishList(lineStatus[lineNumber]);

                                allFishNames = "<" + lineNumToName(lineStatus[lineNumber]) + "> ";
                                for (int i = 0; i < ableFishes.Count; i++)
                                {
                                    int fish_ = ableFishes[i]; // ���� ���� ���� fish_ ��
                                    string itemName = GameManager.Instance.itemDict[0][fish_].itemName; // itemName ��������

                                    // itemName�� allFishNames�� �߰�

                                    allFishNames = allFishNames + "(" + (i + 1).ToString() + ")" + itemName;

                                    if (i != ableFishes.Count - 1)
                                    {
                                        allFishNames += ", ";
                                    }
                                }

                                allFishNames += "\n����Ű : +1����, shift+����Ű : +10���� / Q : ������ / E : ����";

                                Debug.Log(allFishNames);

                                inputPopUpText.GetComponent<TextMeshPro>().text = allFishNames;
                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown(interactionKey))
                            {
                                GameManager.Instance.isInteracting = false;
                                if (inputFish != 0)
                                {
                                    if (controlUI.ReduceActionPoints((0.2f * (float)inputCount[lineNumber]))) ;
                                    {
                                        isWorking[lineNumber] = inputFish;
                                        // Debug.Log(GameManager.Instance.itemDict[0][inputFish].itemName + "��" + inputCount[lineNumber] + "���� ����");
                                        inputPopUpText.GetComponent<TextMeshPro>().text = GameManager.Instance.itemDict[0][inputFish].itemName + " " + inputCount[lineNumber] + "���� ������...";
                                    }
                                }
                            }

                            else if (Input.GetKeyDown(destroyKey))
                            {
                                inputPopUp.SetActive(false);
                                GameManager.Instance.isInteracting = false;
                                isWorking[lineNumber] = 0;
                                inputCount[lineNumber] = 0;
                            }

                            else
                            {
                                for (int i = 1; i <= 7; i++)
                                {
                                    KeyCode key = KeyCode.Alpha0 + i;

                                    // �ش� Ű�� ���ȴ��� Ȯ��
                                    if (Input.GetKeyDown(key))
                                    {
                                        if (i <= ableFishes.Count)
                                        {
                                            if (inputFish != ableFishes[i - 1])
                                                inputCount[lineNumber] = 0;

                                            inputFish = ableFishes[i - 1];

                                            if (Input.GetKey(KeyCode.LeftShift))
                                            {
                                                if (inputCount[lineNumber] <= 40)
                                                {
                                                    inputCount[lineNumber] += 10;
                                                    inputPopUpText.GetComponent<TextMeshPro>().text = allFishNames + "\n" + GameManager.Instance.itemDict[0][inputFish].itemName + " " + inputCount[lineNumber].ToString() + "����";
                                                    Debug.Log(GameManager.Instance.itemDict[0][inputFish].itemName + "��" + inputCount[lineNumber] + "���� �����Ͻðڽ��ϱ�?");

                                                }
                                                else if (inputCount[lineNumber] <= 49)
                                                {
                                                    inputCount[lineNumber] = 50;
                                                    inputPopUpText.GetComponent<TextMeshPro>().text = allFishNames + "\n" + GameManager.Instance.itemDict[0][inputFish].itemName + " " + inputCount[lineNumber].ToString() + "����";
                                                    Debug.Log(GameManager.Instance.itemDict[0][inputFish].itemName + "��" + inputCount[lineNumber] + "���� �����Ͻðڽ��ϱ�?");
                                                }
                                                else
                                                {
                                                    inputPopUpText.GetComponent<TextMeshPro>().text = allFishNames + "\n" + GameManager.Instance.itemDict[0][inputFish].itemName + " " + inputCount[lineNumber].ToString() + "���� (�ִ�)";
                                                    Debug.Log("�� �̻� ���� �� �����ϴ�");
                                                }
                                            }
                                            else
                                            {
                                                if (inputCount[lineNumber] <= 49)
                                                {
                                                    inputCount[lineNumber] += 1;
                                                    inputPopUpText.GetComponent<TextMeshPro>().text = allFishNames + "\n" + GameManager.Instance.itemDict[0][inputFish].itemName + " " + inputCount[lineNumber].ToString() + "����";
                                                }
                                                else
                                                {
                                                    inputPopUpText.GetComponent<TextMeshPro>().text = allFishNames + "\n" + GameManager.Instance.itemDict[0][inputFish].itemName + " " + inputCount[lineNumber].ToString() + "���� (�ִ�)";
                                                    Debug.Log("�� �̻� ���� �� �����ϴ�");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
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

                    string allElementNames = "��ġ ������ ��� : ";
                    for (int i = 0; i < ableElements.Count; i++)
                    {
                        string elementName = facilityNumToName(ableElements[i]); // itemName ��������

                        // itemName�� allFishNames�� �߰�

                        allElementNames = allElementNames + "(" + (i + 1).ToString() + ")" + elementName;

                        if (i != ableElements.Count - 1)
                        {
                            allElementNames += ", ";
                        }
                    }

                    Debug.Log(allElementNames);
                    installPopUpText.GetComponent<TextMeshPro>().text = allElementNames + "\n����Ű : ��� ��ġ / E : ������";
                    installPopUp.SetActive(true);
                    GameManager.Instance.isInteracting = true;

                }

                else if (GameManager.Instance.isInteracting == true)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (controlUI.ReduceActionPoints(15))
                            StartCoroutine(installFacility(0));
                    }
                    else if (Input.GetKeyDown(interactionKey))
                    {
                        GameManager.Instance.isInteracting = false;
                        installPopUp.SetActive(false);
                    }
                    else if (ableElements.Count == 1)
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
                            Debug.Log("Invalid Input");
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (controlUI.ReduceActionPoints(15))
                            StartCoroutine(installFacility(1));
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        if (controlUI.ReduceActionPoints(15))
                            StartCoroutine(installFacility(2));
                    }

                }
            }

            else
            {
                installPopUp.SetActive(false);
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

    List<int> getAbleFishList(int input)
    {
        List<int> result = new List<int>();

        switch (input)
        {
            case 1:
                result.AddRange(new int[] { 10301, 10401, 20301, 20701, 21001, 21101, 21301 });
                break;
            case 2:
                result.AddRange(new int[] { 10201, 20101, 20201, 20601, 20701, 20901, 21201 });
                break;
            case 3:
                result.AddRange(new int[] { 10201, 20501, 20601, 21201, 21301 });
                break;
            case 4:
                result.AddRange(new int[] { 20301, 20701, 21301 });
                break;
            case 5:
                result.AddRange(new int[] { 20501, 20601, 21001, 21101, 21401 });
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
        Debug.Log(lineNumber + "�� ���ο� " + facilityNumToName(ableElements[elementIndex]) + " ��ġ");
        installSound.Play();
        elementStatus[currentTile] = ableElements[elementIndex];        
        CopyTiles(elementTiles[currentTile], elementStatus[currentTile]);        

        if (ableElements[elementIndex] == 4)
        {
            lineStatus[lineNumber] = checkLineType();
            Debug.Log(lineNumToName(lineStatus[lineNumber]) + " �ϼ�");
        }
        yield return new WaitForSeconds(installSound.clip.length / 2);
        currentTile = setCurrentTile();
        if (lineStatus[lineNumber] == 0)
        {
            CopyTiles(elementTiles[currentTile], 9);
            elementTileColliders[currentTile].enabled = true;
        }
        GameManager.Instance.isInteracting = false;        
    }

    void CopyTiles(Tilemap target, int tileNum)
    {        
        Tilemap source = exampleTiles[tileNum];

        // calculate boundary of source Tilemap
        BoundsInt sourceBounds = source.cellBounds;
        Vector3Int targetOrigin = target.origin;  // origin of target Tilemap

        // for each tiles of source Tilemap
        foreach (var pos in sourceBounds.allPositionsWithin)
        {
            // get tile of it and copy it into target Tilemap
            TileBase tile = source.GetTile(pos);

            if (tile != null)
            {
                Vector3Int targetPosition = pos - sourceBounds.position + targetOrigin;
                target.SetTile(targetPosition, tile);
            }
        }
    }

    
    Item makeProduct(int fishID, int lineKind)
    {
        int productID = fishID + lineKind;
        return GameManager.Instance.itemDict[0][productID];
    }

    string facilityNumToName(int num)
    {
        switch (num)
        {
            case 1: return "�õ���";
            case 2: return "������";
            case 3: return "�߰��";
            case 4: return "�����";
            case 5: return "����������";
            case 6: return "�����ռ���";
            case 7: return "������������";
            default: return "Ƣ���";
        }
    }

    string lineNumToName(int num)
    {
        switch (num)
        {
            case 1: return "�õ� ����";
            case 2: return "���� ����";
            case 3: return "���� ����";
            case 4: return "� ����";
            default: return "������ ����";
        }
    }
}
