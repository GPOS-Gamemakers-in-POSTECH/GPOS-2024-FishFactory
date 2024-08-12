using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ActionPoints : fadeInOut
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
            if (actionPoints == 0)
            {
                SceneManager.LoadScene("Indoor");
            }
        }


    }

    protected IEnumerator sleep()
    {
        actionPoints = 100;
        date++;
        StartCoroutine(FadeFunction(0f));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SleepScene");
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