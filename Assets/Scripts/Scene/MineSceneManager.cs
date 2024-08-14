// script for designing mining scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MineSceneManager : fadeInOut
{
    public AudioSource miningAudio;

    void Start()
    {
        // load minmul scene after waiting 1 second
        StartCoroutine(delayedSceneLoad("Ocean", miningAudio));
    }
}
