using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryUiManager : MonoBehaviour
{
    public Button fishesButton;
    public Button productsButton;
    public Button toolsButton;
    public Button facilitiesButton;
    public Button closeButton;
    public Button openButton;

    public Button testButton;
    private int test;
    public GameObject inventoryUi;

    public RectTransform targetRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        fishesButton.onClick.AddListener(scrollFishes);
        productsButton.onClick.AddListener(scrollProducts);
        toolsButton.onClick.AddListener(scrollTools);
        facilitiesButton.onClick.AddListener(scrollFacilities);
        openButton.onClick.AddListener(openInventory);
        closeButton.onClick.AddListener(closeInventory);
        testButton.onClick.AddListener(testFunc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void scrollFishes()
    {
        targetRectTransform.anchoredPosition = new Vector2(targetRectTransform.anchoredPosition.x, 210);
    }

    void scrollProducts()
    {
        targetRectTransform.anchoredPosition = new Vector2(targetRectTransform.anchoredPosition.x, 410);
    }

    void scrollTools()
    {
        targetRectTransform.anchoredPosition = new Vector2(targetRectTransform.anchoredPosition.x, 680);
    }

    void scrollFacilities()
    {
        targetRectTransform.anchoredPosition = new Vector2(targetRectTransform.anchoredPosition.x, 910);
    }

    void openInventory()
    {
        inventoryUi.SetActive(true);
    }

    void closeInventory()
    {
        inventoryUi.SetActive(false);
    }

    void testFunc()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            test+=10;
            Debug.Log(test);
        }
        else
        {
            test += 1;
            Debug.Log(test);
        }
    }
}
