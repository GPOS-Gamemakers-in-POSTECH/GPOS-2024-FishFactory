using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionPoints : MonoBehaviour
{
    public static int actionPoints = 100;
    public static int date = 0;
    private int daysinOneSeason = 5;

    public TextMeshProUGUI ActionPointsText;
    public TextMeshProUGUI DateText;

    void Start()
    {
        UpdateActionPointsUI();
        UpdateDateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ReduceActionPoints(10);

        if (Input.GetKeyDown(KeyCode.T))
            ReduceActionPoints(20);
    }

    void ReduceActionPoints(int amount)
    {
        if (actionPoints < amount)
            Debug.Log("Running out of Action Points!");
        else
        {
            actionPoints -= amount;
            actionPoints = Mathf.Clamp(actionPoints, 0, 100);
            UpdateActionPointsUI();
        }
    }

    protected void sleep()
    {
        actionPoints = 100;
        date++;
        // Debug.Log("Sleeped Well! All Action Points Restored.");
        UpdateActionPointsUI();
        UpdateDateUI();
    }

    void UpdateActionPointsUI()
    {
        if (ActionPointsText != null)
        {
            ActionPointsText.text = actionPoints.ToString() + " / 100";
        }
    }

    void UpdateDateUI()
    {
        if (DateText != null)
        {
            switch(date / daysinOneSeason)
            {
                case 0: DateText.text = "SPRING - " + ((date % daysinOneSeason) + 1).ToString(); break;
                case 1: DateText.text = "SUMMER - " + ((date % daysinOneSeason) + 1).ToString(); break;
                case 2: DateText.text = "FALL - " + ((date % daysinOneSeason) + 1).ToString(); break;
                case 3: DateText.text = "WINTER - " + ((date % daysinOneSeason) + 1).ToString(); break;
                default: DateText.text = "Game End!"; break;
            }
        }
    }
}