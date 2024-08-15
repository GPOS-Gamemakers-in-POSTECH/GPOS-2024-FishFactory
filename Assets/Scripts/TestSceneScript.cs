using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneScript : MonoBehaviour
{
    JsonManager jsonManager;
    Dictionary<int, Item> itemDict;

    // Start is called before the first frame update
    void Start()
    {
        itemDict = new Dictionary<int, Item>();
        jsonManager=FindObjectOfType<JsonManager>();
        jsonManager.LoadJson(itemDict);

        Debug.Log(itemDict[10100].itemName);
    }
}
