using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionPoints : MonoBehaviour
{
    private static int actionPoints = 100;

    public TextMeshProUGUI ActionPointsText;

    void Start()
    {
        UpdateActionPointsUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ReduceActionPoints(10);

        if (Input.GetKeyDown(KeyCode.T))
            ReduceActionPoints(20);

        if (Input.GetKeyDown(KeyCode.B))
            sleep();
    }

    void ReduceActionPoints(int amount)
    {
        if (actionPoints < amount)
            Debug.Log("Running out of Action Points!");
        else
        {
            actionPoints -= amount;
            actionPoints = Mathf.Clamp(actionPoints, 0, 100);
            UpdateActionPointsUI();  // UI 업데이트
        }
    }

    void sleep()
    {
        actionPoints = 100;
        Debug.Log("Sleeped Well! All Action Points Restored.");
        UpdateActionPointsUI();  // UI 업데이트
    }

    void UpdateActionPointsUI()
    {
        if (ActionPointsText != null)
        {
            ActionPointsText.text = actionPoints.ToString() + "/100";
        }
    }
}