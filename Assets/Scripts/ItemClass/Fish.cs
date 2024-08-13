using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum FishType
{
    freshWater = 0,
    river = 0,
    saltWater = 1,
    ocean = 1,
    inDoor = 2,
    both = 2
}
public class GrowthRateData
{
    public float avg;
    public float minVar;
    public float maxVar;
    public float baseGrowthRate;
    public float GrowthRateModifier(float curVar)
    {
        return baseGrowthRate * Mathf.Min(Mathf.Min((maxVar - Mathf.Abs(curVar - avg) + minVar) / (maxVar+minVar/2), 1), Convert.ToSingle(maxVar > Mathf.Abs(curVar - avg)));
    }
}
[System.Serializable]
public class Fish 
{
    public string fishName;
    public int fishTier;//1~3 for freshwater, 1~5 for ocean
    public FishType fishType;//river/ocean/indoor as 0/1/2
    public bool isUnlocked;//Is unlocked by player?
    public GrowthRateData waterOxygen;
    public GrowthRateData waterDegree;
    public Vector2 growthTime;//hatchTime/grownTime

    public float baseGrowthRate;//base growth rate
    public float minGrowthRate;//minimum growth rate before the countdown starts ticking
    public int expirationDate;//date before fish dies

    public (float curGrowthRate, bool isExpire) CalcGrowthRate(FishType farmType, int farmTier, float curOxygen, float curDegree, bool efficiencyMax = false, float eventModifier=1.0f, bool savingThrow=false)
    {
        float curGrowthRate = baseGrowthRate;
        if (farmTier - 2 >= fishTier) curGrowthRate *= 1.4f;//if farm tier is way higher, gets a big bonus
        //else if (farmTier - 1 >= fishTier) curGrowthRate *= 1.1f;
        if (farmType == FishType.both && fishType != FishType.both) curGrowthRate *= 0.8f;//if outdoor fish is grown inside, gets 0.8x penalty
        if (farmType != FishType.both && fishType != farmType) curGrowthRate *= 0.0f;//wrong placement gets a 0x modifier cause fish cant grow there
        if (efficiencyMax) curGrowthRate *= waterOxygen.baseGrowthRate * waterDegree.baseGrowthRate;//if efficiency is max due to event(or other stuff) gets max oxygen/degree bonus
        else curGrowthRate *= waterOxygen.GrowthRateModifier(curOxygen) * waterDegree.GrowthRateModifier(curDegree);//if its not, it is calculated

        return (curGrowthRate * eventModifier, curGrowthRate < minGrowthRate&&!savingThrow);
    }

    public void DebugPrintInfo()
    {
        Debug.Log($"Fish Name: {fishName}");
        Debug.Log($"Fish Tier: {fishTier}");
        Debug.Log($"Fish Type: {fishType}");
        Debug.Log($"Is Unlocked: {isUnlocked}");

        Debug.Log($"Water Oxygen - Avg: {waterOxygen.avg}, MinVar: {waterOxygen.minVar}, MaxVar: {waterOxygen.maxVar}, Base Growth Rate: {waterOxygen.baseGrowthRate}");
        Debug.Log($"Water Degree - Avg: {waterDegree.avg}, MinVar: {waterDegree.minVar}, MaxVar: {waterDegree.maxVar}, Base Growth Rate: {waterDegree.baseGrowthRate}");

        Debug.Log($"Growth Time - Hatch Time: {growthTime.x}, Grown Time: {growthTime.y}");

        Debug.Log($"Base Growth Rate: {baseGrowthRate}");
        Debug.Log($"Min Growth Rate: {minGrowthRate}");
        Debug.Log($"Expiration Date: {expirationDate}");
    }
}
