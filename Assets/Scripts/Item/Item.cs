using UnityEngine;
using System;
using System.Diagnostics;

[Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public bool waterType;
    public int itemTier;
    public int time;
    public double feedAmount;
    public int sellPrice;
    public int buyPrice;
    public int power;

    public void ShowItemInfo()
    {
        string debugLine = itemID.ToString() + " ; " + itemName + " : /t";
        int itemType = itemID / 10000;
        if (itemType == 1 || itemType == 2 || itemType == 4)
            debugLine += "ItemTier:" + itemTier.ToString();
        if ((itemType == 1 && itemID % 10 == 0) || (itemType == 2 && itemID % 10 == 0) || itemType == 9)
            debugLine += " | " + time.ToString();
        if (itemType >= 1 && itemType <= 2)
            debugLine += ", " + (!waterType ? "Freshwater" : "Ocean") + ", FeedAmount: " + feedAmount.ToString();
        if (itemType >= 1 && itemType <= 4)
            debugLine += " | " + sellPrice.ToString() + "/" + buyPrice.ToString();
        if (itemType == 9)
            debugLine += " Power:" + power.ToString();
        UnityEngine.Debug.Log(debugLine);
    }


    public void GeneratePower(int increasePower) { power += increasePower; } // Increase Power Value

    public void DecreasePower(int decreasePower) { power -= decreasePower; } // Decrease Power Value

    public static int PowerConsumption(Item[] items)
    {
        int totalPower=0;
        if(items.Length == 0) return 0;
        for (int i = 0; i < items.Length; i++)
        {
            totalPower += items[i].power;
        }
        return totalPower;
    }
}

[Serializable]
public class ItemData
{
    public Item[] fishes;
    public Item[] tools;
    public Item[] facilities;
}