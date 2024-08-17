// script for control AP system

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ActionPoints : fadeInOut
{
    // how many days in one season
    protected int daysInOneSeason = 1;
    // amount of max AP
    protected int maxActionPoints = 100;
    // current season
    protected int currentSeason;

    // initialize UI values
    public static int actionPoints = 100;
    public static int date = 0;

    // check if interaction is occuring
    protected static int isDoingInteract = 0;

    // coroutine to do sleep
    protected IEnumerator sleep()
    {
        // fill the AP to max and add date
        actionPoints = maxActionPoints;
        date++;

        // fade out and move to SleepScene
        StartCoroutine(FadeFunction(0f));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SleepScene");
    }
}