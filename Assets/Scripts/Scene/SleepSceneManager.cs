// script for control after sleep

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SleepSceneManager : fadeInOut
{
    public AudioSource wakeupAudio;

    void Start()
    {
        // load indoor scene after waiting 1 second
        StartCoroutine(delayedSceneLoad("MinMul", wakeupAudio));
    }
}
