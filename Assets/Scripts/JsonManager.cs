using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    // Load Fish Data
    public void LoadFishJson(Dictionary<int, Fish> fishDict)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Fish");
        FishData fishData = JsonUtility.FromJson<FishData>(textAsset.text);
        foreach (Fish fish in fishData.fishes) { fishDict.Add(fish.itemID, fish); }
        return;
    }

    // Load Recipe Data
    public void LoadRecipeJson(Dictionary<int, Recipe> recipes)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Recipe");
        return;
    }
}