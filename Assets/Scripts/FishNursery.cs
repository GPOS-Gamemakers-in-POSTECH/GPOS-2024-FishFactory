using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NurseryParts;

public class FishNursery
{
    // NurseryTier: 양식장의 등급
    private int NurseryTier;

    // IsNurseryAvailable: 양식장이 사용 가능한지 여부
    public bool IsNurseryAvailable { get; private set; }

    // FishTarget: 양식하는 물고기
    public string FishTarget { get; set; }

    // FishPossible: 양식 가능한 물고기 종류
    public List<string> FishPossible { get; set; }

    // PartsList: 장착된 부품 목록
    public List<NurseryParts> PartsList { get; set; }

    // Temperature: 수온
    public double Temperature { get; set; }

    // OxygenRate: 용존 산소량
    public double OxygenRate { get; set; }

    // GrowthRate: 성장률
    public int GrowthRate { get; set; }

    // ExpirationDate: 폐사 일수 확인
    public int ExpirationDate { get; set; }

    // 생성자
    public FishNursery()
    {
        NurseryTier = 1; 
        IsNurseryAvailable = true;
        FishPossible = new List<string>(); // 리스트 초기화
        PartsList = new List<NurseryParts>(); // 리스트 초기화
        Temperature = 20.0; // 기본 수온 예시 값
        OxygenRate = 8.0; // 기본 용존 산소량 예시 값
        GrowthRate = 0; // 기본 성장률
        ExpirationDate = 0; // 폐사 일수
    }


    public void ShowNurseryInfo()
    {
        Console.WriteLine($"Nursery Tier: {NurseryTier}");
        Console.WriteLine($"Is Nursery Available: {IsNurseryAvailable}");
        Console.WriteLine($"Fish Target: {FishTarget}");
        Console.WriteLine($"Fish Possible: {string.Join(", ", FishPossible)}");
        Console.WriteLine($"Temperature: {Temperature}");
        Console.WriteLine($"Oxygen Rate: {OxygenRate}");
        Console.WriteLine($"Growth Rate: {GrowthRate}");
        Console.WriteLine($"Expiration Date: {ExpirationDate} days");
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
            Console.WriteLine("Warning: Missing essential parts!");
            ExpirationDate++;
        }
        else
        {
            Console.WriteLine("All essential parts are available.");
        }
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

    bool IsUpgradePossible()//티어 상승 조건 확인
    {
        if(CheckNecessaryParts())
        {
            //추가부품 확인 코드

            return true;
        }
        return false;
    }

    

}


public class FreshWaterNursery : FishNursery
{
    
}

public class SeaWaterNursery : FishNursery
{
    
}

public class InsideNursery : FishNursery
{

}