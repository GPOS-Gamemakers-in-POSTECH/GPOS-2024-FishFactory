// script for selling UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sellingManager : ActionPoints
{
    // button to close UI
    public Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(closeUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void closeUI()
    {
        gameObject.SetActive(false);
        isDoingInteract = 0;
    }
}