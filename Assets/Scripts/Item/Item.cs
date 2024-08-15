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
}

[Serializable]
public class ItemData
{
    public Item[] fishes;
    public Item[] tools;
    public Item[] facilities;
}