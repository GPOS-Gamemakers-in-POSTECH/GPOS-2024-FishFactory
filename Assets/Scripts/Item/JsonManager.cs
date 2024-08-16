using System.Collections.Generic;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    // Load Item Data
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