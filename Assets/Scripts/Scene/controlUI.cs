using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class controlUI : ActionPoints
{
    // UI texts
    public TextMeshProUGUI ActionPointsText;
    public TextMeshProUGUI DateText;

    // update UI when starting the scene
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

    // function to update AP
    void ReduceActionPoints(int amount)
    {
        // if require more than remaining AP, action is not happens
        if (actionPoints < amount)
            Debug.Log("Running out of Action Points!");

        // if AP is enough, reduce AP and update UI
        else
        {
            actionPoints -= amount;
            UpdateActionPointsUI();

            // if AP becomes zero, move to scene where bed is located
            if (actionPoints == 0)
            {
                SceneManager.LoadScene("Indoor");
            }
        }
    }

    // function to update AP UI
    void UpdateActionPointsUI()
    {
        // express AP as "amount / max amount"
        if (ActionPointsText != null)
        {
            ActionPointsText.text = actionPoints.ToString() + " / " + maxActionPoints.ToString();
        }
    }

    // function to update Date UI
    void UpdateDateUI()
    {
        // express Date as "season - day"
        if (DateText != null)
        {
            switch (date / daysInOneSeason)
            {
                case 0: DateText.text = "SPRING - " + ((date % daysInOneSeason) + 1).ToString(); break;
                case 1: DateText.text = "SUMMER - " + ((date % daysInOneSeason) + 1).ToString(); break;
                case 2: DateText.text = "FALL - " + ((date % daysInOneSeason) + 1).ToString(); break;
                case 3: DateText.text = "WINTER - " + ((date % daysInOneSeason) + 1).ToString(); break;
                default: DateText.text = "Game End!"; break;
            }
        }
    }
}
