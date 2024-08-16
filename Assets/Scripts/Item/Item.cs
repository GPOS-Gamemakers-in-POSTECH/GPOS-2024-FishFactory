using UnityEngine;
using System;

[Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public bool waterType;
    public int itemTier;
    public int growTime;
    public double feedAmount;
    public int sellPrice;
    public int buyPrice;
    public int power;

    public void ShowItemInfo()
    {
        Debug.Log(itemID);
    }

    public void GeneratePower(int increasePower) { power += increasePower; } // Increase Power Value
    public void DecreasePower(int decreasePower) { power -= decreasePower; } // Decrease Power Value
}

[Serializable]
public class ItemData
{
    public Item[] fishes;
    public Item[] tools;
    public Item[] facilities;
}