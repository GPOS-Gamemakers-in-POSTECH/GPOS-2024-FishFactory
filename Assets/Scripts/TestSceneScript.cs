using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneScript : MonoBehaviour
{
    JsonManager jsonManager;
    Dictionary<int, Fish> fishDict;

    // Start is called before the first frame update
    void Start()
    {
        fishDict = new Dictionary<int, Fish>();
        jsonManager=FindObjectOfType<JsonManager>();
        jsonManager.LoadFishJson(fishDict);

        Debug.Log(fishDict[10100].fishName);
    }
}
