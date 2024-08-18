using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFarm
{
    public int farmTear;
    public Item fish;
    public int fishAmount;
    public Item[] parts;
    public bool[] isPartsOn;
    public int dieCount;
    public int growCount;

    // Constructor
    public FishFarm(int farmTear, Item fish, Item[] parts, int fishAmount)
    {
        this.farmTear = farmTear;
        this.fish = fish;
        this.parts = parts;
        this.fishAmount = fishAmount;
        isPartsOn = new bool[]{false, false, false, false};
        dieCount = 0;
        growCount = 0;
    }

    // Show the Information of fish farm
    public void ShowFishFarmInfo()
    {
        Debug.Log(farmTear);
        Debug.Log(fish.itemID);
        Debug.Log(fish.itemName);
        Debug.Log(fishAmount);
        Debug.Log(isPartsOn);
    }

    // Add New Parts to fish farm
    public void AddParts(Item newParts)
    {
        for (int i = 0; i < 4; i++)
        {
            if (parts[i].itemID / 1000 == newParts.itemID / 1000)
            {
                if (parts[i].itemID == newParts.itemID) { Debug.Log("�̹� ������ �����Դϴ�."); }
                else if (parts[i].itemID < newParts.itemID)
                {
                    parts[i] = newParts;
                    Debug.Log("���� ��ü �Ϸ�");
                }
                else { Debug.Log("���� Ƽ���� �����Դϴ�."); }

                return;
                    
            }
            else { Debug.Log("������ �� ���� �������Դϴ�."); }
        }

        return;
    }

    // Check Parts of fish farm
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
