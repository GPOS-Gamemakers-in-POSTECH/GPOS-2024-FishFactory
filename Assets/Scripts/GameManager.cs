using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isInteracting; // Check If Player is Interacting
    public FishTankData[] freshFishTanks = new FishTankData[9];
    public FishTankData[] oceanFishTanks = new FishTankData[20];
    public FishTankData[] indoorFishTanks = new FishTankData[9];
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

            for (int i = 0; i < 9; i++) { freshFishTanks[i] = new FishTankData(); }
            for (int i = 0; i < 20; i++) { oceanFishTanks[i] = new FishTankData(); }
            for (int i = 0; i < 9; i++) { indoorFishTanks[i] = new FishTankData(); }

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
