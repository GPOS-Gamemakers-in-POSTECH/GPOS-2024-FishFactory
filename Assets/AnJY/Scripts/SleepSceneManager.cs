using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepSceneManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(delayedSceneLoad());
    }

    IEnumerator delayedSceneLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Indoor");
    }
}
