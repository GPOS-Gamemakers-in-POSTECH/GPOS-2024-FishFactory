using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeInOut : MonoBehaviour
{
    public Image fadeImage;
    private float fadeDuration = 1f;

    protected IEnumerator FadeAndLoadScene(string sceneName)
    {
        // fade out
        yield return StartCoroutine(FadeFunction(0f));

        // load the new scene
        SceneManager.LoadScene(sceneName);
    }

    protected IEnumerator delayedSceneLoad(string sceneName)
    {
        StartCoroutine(FadeFunction(1f));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }



    protected IEnumerator FadeFunction(float index) // 0 : fade out, 1 : fade in
    {
        float elapsedTIme = 0f;
        Color color = fadeImage.color;
        while (elapsedTIme < fadeDuration)
        {
            elapsedTIme += Time.deltaTime;
            color.a = Mathf.Lerp(index, 1f - index, elapsedTIme / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
