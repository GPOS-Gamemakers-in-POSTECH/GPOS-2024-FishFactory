using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFarm
{
    public int farmTear;
    public Item fish;
    public int[] fishAmount;
    public bool[] isFacilityOn;

    public FishFarm(int farmTear, Item fish, int[] fishAmount, bool[] isFacilityOn)
    {
        this.farmTear = farmTear;
        this.fish = fish;
        this.fishAmount = fishAmount;
        this.isFacilityOn = isFacilityOn;
    }

    public void ShowFishFarmInfo()
    {
        Debug.Log(farmTear);
        Debug.Log(fish.itemID);
        Debug.Log(fish.itemName);
        Debug.Log(fishAmount);
        Debug.Log(isFacilityOn);
    }
}
