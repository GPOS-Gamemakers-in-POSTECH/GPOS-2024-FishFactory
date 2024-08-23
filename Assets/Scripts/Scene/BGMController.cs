// script for control game BGM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    // singletone instance
    private static BGMController Instance;

    public AudioSource bgmSource;
    public AudioClip springBGM;
    public AudioClip summerBGM;
    public AudioClip fallBGM;
    public AudioClip winterBGM;

    // check if current scene is sleepScene
    private int isInSleepScene = 0;

    public int totalDate;
    int seasonDate = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        totalDate = GameManager.Instance.totalDate;
        playBGM();
    }

    void Update()
    {
        // if moved to sleepScene, stop BGM and restart playBGM after waiting for a moment
        if (SceneManager.GetActiveScene().name == "SleepScene" && isInSleepScene == 0)
        {
            StartCoroutine(BGMseasonChange());
            isInSleepScene = 1; // this makes update happens one time in sleepScene
        }
    }

    // insert correct BGM clip into bgmSource and play it
    void playBGM()
    {
        switch (totalDate / seasonDate)
        {
            case 0: // spring
                bgmSource.clip = springBGM; break;
            case 1: // summer
                bgmSource.clip = summerBGM; break;
            case 2: // fall
                bgmSource.clip = fallBGM; break;
            default: // winter
                bgmSource.clip = winterBGM; break;
        }
        bgmSource.Play();
    }

    // stop BGM and restart playBGM after waiting for a moment
    IEnumerator BGMseasonChange()
    {
        bgmSource.Stop();
        yield return new WaitForSeconds(2);
        isInSleepScene = 0; // reset the value
        playBGM();
    }
}
