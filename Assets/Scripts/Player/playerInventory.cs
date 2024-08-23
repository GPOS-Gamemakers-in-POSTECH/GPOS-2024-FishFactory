using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public List<KeyValuePair<int, int>> itemAndCount = new List<KeyValuePair<int, int>>();
    public JsonManager jsonManager;
    Dictionary<int, Item>[] itemDict = new Dictionary<int, Item>[3];

    void Awake()
    {


        for (int i = 0; i < itemDict.Length; i++)
        {
            itemDict[i] = new Dictionary<int, Item>();
        }

        jsonManager.LoadJson(itemDict);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addToInventory(int itemID, int count)
    {
        // Flag to check if itemID is already in the list
        bool itemExists = false;

        // Iterate through the list to find the itemID
        for (int i = 0; i < itemAndCount.Count; i++)
        {
            if (itemAndCount[i].Key == itemID)
            {
                // If itemID is found, update its count
                itemAndCount[i] = new KeyValuePair<int, int>(itemID, itemAndCount[i].Value + count);
                itemExists = true;                
                break;
            }
        }

        // If itemID was not found, add it as a new item
        if (!itemExists)
        {
            itemAndCount.Add(new KeyValuePair<int, int>(itemID, count));
        }
    }

    public void PrintInventory()
    {
        foreach (KeyValuePair<int, int> item in itemAndCount)
        {
            Debug.Log("Item ID: " + item.Key + ", Count: " + item.Value);
        }
        Debug.Log("----");
    }

    public Item findItemWithID(int itemID)
    {
        for (int i = 0; i < itemDict.Length; i++)
        {
            foreach (KeyValuePair<int, Item> kvp in itemDict[i])
            {
                if (kvp.Key == itemID)
                    return kvp.Value;
            }
        }
        return null;
    }


}
