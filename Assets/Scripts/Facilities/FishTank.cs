using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTank : MonoBehaviour
{
    public Item fish;
    public int fishAmount;
    public Item[] parts;
    public bool[] isPartsOn;
    public int dieCount;
    public int growCount;

    // Constructor
    public FishTank()
    {
        fish = null;
        fishAmount = 0;
        parts = new Item[] { null, null, null, null };
        isPartsOn = new bool[] { false, false, false, false };
        dieCount = 0;
        growCount = 0;
    }

    // Show the Information of fish Tank
    public void ShowFishTankInfo()
    {
        Debug.Log(fish.itemID);
        Debug.Log(fish.itemName);
        Debug.Log(fishAmount);
        Debug.Log(isPartsOn);
    }

    // Add New Fishes to Fish Tank
    public void AddFish(Item fish, int fishAmount)
    {
        if (this.fish == null)
        {
            this.fish = fish;
            this.fishAmount = fishAmount;
        }
        else { Debug.Log("Fish Already Exists."); }

        return;
    }

    // Gather Grown Fishes
    public void GatherFish()
    {
        if (growCount >= fish.growTime) { Debug.Log("Gathered"); }
        else if (dieCount >= 3) { Debug.Log("Died"); }
        else { Debug.Log("Not Yet"); }
    }

    // Add New Parts to Fish Tank
    public void AddParts(Item newParts)
    {
        for (int i = 0; i < 4; i++)
        {
            if (parts[i].itemID / 1000 == newParts.itemID / 1000)
            {
                if (parts[i].itemID == newParts.itemID) { Debug.Log("Parts Already Exists"); }
                else if (parts[i].itemID < newParts.itemID)
                {
                    parts[i] = newParts;
                    Debug.Log("Changed");
                }
                else { Debug.Log("Low Tier"); }

                return;
                    
            }
            else { Debug.Log("Invalid Item"); }
        }

        return;
    }

    // Check Parts of Fish Tank
    public void CheckPartsOn()
    {
        for (int i = 0; i < 4; i++)
        {
            if (parts[i].itemID != 0) { isPartsOn[i] = true; }
            else { isPartsOn[i] = false; }
        }

        return;
    }

    // Feed Fishes
    public void FeedFish()
    {
        if (isPartsOn[3] == true)
        {
            parts[3].feedAmount -= fish.feedAmount * fishAmount;
        }
    }

    // Check fish's grown days or days to die
    public void DecideFishHealth()
    {
        if (isPartsOn[0] == false && isPartsOn[1] == false) { dieCount += 1; }
        else { dieCount = 0; growCount += 1; }

        if (dieCount == 3) { }

        return;
    }
}
