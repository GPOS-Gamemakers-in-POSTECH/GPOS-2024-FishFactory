using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    // Load Item Data
    public void LoadJson(Dictionary<int, Item> itemDict)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Item");
        ItemData itemData = JsonUtility.FromJson<ItemData>(textAsset.text);
        foreach (Item item in itemData.fishes) { itemDict.Add(item.itemID, item); }
        return;
    }
}