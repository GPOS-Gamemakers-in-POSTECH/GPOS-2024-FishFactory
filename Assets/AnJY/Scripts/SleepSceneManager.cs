using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SleepSceneManager : fadeInOut
{
    void Start()
    {
        StartCoroutine(delayedSceneLoad("Indoor"));
    }
}
