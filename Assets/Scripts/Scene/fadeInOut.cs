// script to implement fade in/out

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeInOut : MonoBehaviour
{
    // black image to hide the screen
    public Image fadeImage;

    // time taken to fade in/out
    private float fadeDuration = 1f;

    // function to use fade in/out    
    protected IEnumerator FadeFunction(float index) // index value - 0 : fade out, 1 : fade in
    {
        // counting time past
        float elapsedTIme = 0f;
        Color color = fadeImage.color;

        while (elapsedTIme < fadeDuration)
        {
            // fade out : color.a changes from 0 to 1
            // fade in : oposite
            elapsedTIme += Time.deltaTime;
            color.a = Mathf.Lerp(index, 1f - index, elapsedTIme / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    // function to fade out current scene and load new scene with name sceneName
    protected IEnumerator FadeAndLoadScene(string sceneName)
    {
        // fade out
        yield return StartCoroutine(FadeFunction(0f));

        // load the new scene
        SceneManager.LoadScene(sceneName);
    }

    // function to fade in and delay, and load current scene
    protected IEnumerator delayedSceneLoad(string sceneName)
    {
        StartCoroutine(FadeFunction(1f));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

}
