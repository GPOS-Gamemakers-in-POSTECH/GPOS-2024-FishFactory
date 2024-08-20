using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryUiManager : MonoBehaviour
{
    public Button button1;
    public Button button2;

    public RectTransform targetRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(scrollInventory1);
        button2.onClick.AddListener(scrollInventory2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void scrollInventory1()
    {
        targetRectTransform.anchoredPosition = new Vector2(targetRectTransform.anchoredPosition.x, 0);
    }

    void scrollInventory2()
    {
        targetRectTransform.anchoredPosition = new Vector2(targetRectTransform.anchoredPosition.x, 445);
    }
}
