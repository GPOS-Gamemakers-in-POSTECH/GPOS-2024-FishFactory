using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isInteracting; // Check If Player is Interacting
    public bool[] freshFishTank = new bool[9];
    public bool[] oceanFishTank = new bool[20];
    public bool[] indoorFishTank = new bool[9];
    public FishTankData[] fishTanks = new FishTankData[9];
    public Dictionary<int, Item>[] itemDict = new Dictionary<int, Item>[3];


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            itemDict[0] = new Dictionary<int, Item>();
            itemDict[1] = new Dictionary<int, Item>();
            itemDict[2] = new Dictionary<int, Item>();

            fishTanks[0] = new FishTankData();
            fishTanks[1] = new FishTankData();
            fishTanks[2] = new FishTankData();
            fishTanks[3] = new FishTankData();
            fishTanks[4] = new FishTankData();
            fishTanks[5] = new FishTankData();
            fishTanks[6] = new FishTankData();
            fishTanks[7] = new FishTankData();
            fishTanks[8] = new FishTankData();

            LoadJson(itemDict);
        }
        else { Destroy(gameObject); }
    }

    // Load Item Data from Json
    public void LoadJson(Dictionary<int, Item>[] itemDict)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Item");
        ItemData itemData = JsonUtility.FromJson<ItemData>(textAsset.text);

        foreach (Item item in itemData.fishes) { itemDict[0].Add(item.itemID, item); }
        foreach (Item item in itemData.tools) { itemDict[1].Add(item.itemID, item); }
        foreach (Item item in itemData.facilities) { itemDict[2].Add(item.itemID, item); }

        return;
    }
}
