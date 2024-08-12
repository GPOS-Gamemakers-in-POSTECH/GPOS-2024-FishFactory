using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NurseryParts;

public class FishNursery
{
    // NurseryTier: 양식장의 등급
    private int NurseryTier;
    private int MaxTier;

    
    public bool IsNurseryAvailable { get; set; }// IsNurseryAvailable: 양식장이 사용 가능한지 여부
    public List<NurseryParts> PartsList { get; set; }// PartsList: 장착된 부품 목록
    public double Temperature { get; set; }// Temperature: 수온
    public double OxygenRate { get; set; } // OxygenRate: 용존 산소량

    
    public int FeedAmount { get; set; }//FeedAmount: 양식장 현 사료량
    public int MaxFeed { get; set; }// MaxFeed: 최대 사료량
    public int FishPopulation { get; set; }// FishPopulation : 물고기 개체 수, 차후 수정


    //* 아래 코드는 수정

    // FishTarget: 양식하는 물고기
    public string FishTarget { get; set; }

    // FishPossible: 양식 가능한 물고기 종류
    public List<string> FishPossible { get; set; }

    // ExpirationDate: 폐사 일수 확인
    public int ExpirationDate { get; set; }
    // GrowthRate: 성장률
    public int GrowthRate { get; set; }

    // 생성자
    public FishNursery()
    {
        NurseryTier = 1;
        MaxTier = 5;
        IsNurseryAvailable = true;
        FishPossible = new List<string>(); // 리스트 초기화
        PartsList = new List<NurseryParts>(); // 리스트 초기화

        FeedAmount = 0;
        MaxFeed = 100; //최대 사료량 예시 값
        Temperature = 20.0; // 기본 수온 예시 값
        OxygenRate = 8.0; // 기본 용존 산소량 예시 값

        FishPopulation = 0;
        GrowthRate = 0; // 기본 성장률
        ExpirationDate = 0; // 폐사 일수
        
    }
    


    public void ShowNurseryInfo()
    {
        Debug.Log($"Nursery Tier: {NurseryTier}");
        Debug.Log($"Is Nursery Available: {IsNurseryAvailable}");
        Debug.Log($"Fish Target: {FishTarget}");
        Debug.Log($"Fish Possible: {string.Join(", ", FishPossible)}");
        Debug.Log($"Temperature: {Temperature}");
        Debug.Log($"Oxygen Rate: {OxygenRate}");
        Debug.Log($"Feed Amount: {FeedAmount}/{MaxFeed}");
        Debug.Log($"Fish Population: {FishPopulation}");
        Debug.Log($"Growth Rate: {GrowthRate}");
        Debug.Log($"Expiration Date: {ExpirationDate} days");
    }

    // 필수 부품 확인
    public void CheckNecessaryParts()
    {
        bool hasOxygenController = false;
        bool hasTemperatureController = false;

        foreach (var parts in PartsList)
        {
            if (parts.PartsName == "OxygenController")
            {
                hasOxygenController = true;
            }
            else if (parts.PartsName == "TemperatureController")
            {
                hasTemperatureController = true;
            }
        }

        if (!hasOxygenController || !hasTemperatureController)
        {
            //Debug.Log("Warning: Missing essential parts!");
            ExpirationDate++;
        }
        else
        {
            //Debug.Log("All essential parts are available.");
        }
    }

    // 사료 추가
    public bool AddFeed(int amount)
    {
        if (FeedAmount + amount > MaxFeed)
        {
            Debug.Log("Cannot add more feed, maximum capacity reached.");//최대 사료량 도달
            return false;
        }

        FeedAmount += amount;
        return true;
    }

    // 부품 추가
    public bool AddParts(NurseryParts parts)
    {
        // 중복 부품 확인
        if (PartsList.Exists(p => p.PartsName == parts.PartsName))
        {
            return false; // 파츠 중복
        }
        PartsList.Add(parts);
        return true;
    }

    void IsUpgradePossible()//티어 상승 조건 확인
    {
        if(CheckNecessaryParts() && Nursery < MaxTier)
        {
            //추가부품 확인 코드
            
            NurseryTier ++;
            return;
        }
        return;
    }

    

}


public class FreshWaterNursery : FishNursery
{
    public FreshWaterNursery() : base()
    {
        MaxTier = 3;
        
    }
}

public class SeaWaterNursery : FishNursery
{
    public SeaWaterNursery() : base()
    {
        //
    }
}

public class InsideNursery : FishNursery
{
    public InsideNursery() : base()
    {
        //
    }
}