using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFarm
{
    public int farmTear;
    public Item fish;
    public int[] fishAmount;
    public bool[] isFacilityOn;
    public int dieCount;
    public int growCount;

    public FishFarm(int farmTear, Item fish, int[] fishAmount, bool[] isFacilityOn)
    {
        this.farmTear = farmTear;
        this.fish = fish;
        this.fishAmount = fishAmount;
        this.isFacilityOn = isFacilityOn;
        dieCount = 0;
        growCount = 0;
    }

    public void ShowFishFarmInfo()
    {
        Debug.Log(farmTear);
        Debug.Log(fish.itemID);
        Debug.Log(fish.itemName);
        Debug.Log(fishAmount);
        Debug.Log(isFacilityOn);
    }

    public void CalculateCount()
    {
        if (isFacilityOn[0] == false && isFacilityOn[1] == false) { dieCount += 1; }
        else { growCount += 1; }

        if (dieCount == 3) { }
    }
}
