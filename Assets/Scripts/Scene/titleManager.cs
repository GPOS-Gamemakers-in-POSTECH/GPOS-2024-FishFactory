// script to control elements of title scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class titleManager : MonoBehaviour
{
    // buttons
    public Button startButton;
    public Button closeButton;
    public AudioSource titleBGM;

    // Start is called before the first frame update
    void Start()
    {
        // start bgm and set button
        titleBGM.Play();
        startButton.onClick.AddListener(gameStart);
        closeButton.onClick.AddListener(gameClose);
    }

    void gameStart()
    {
        SceneManager.LoadScene("MinMul");
    }

    void gameClose()
    {
        // close the game in both unity build and application
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // 빌드된 게임에서 종료
                Application.Quit();
        #endif
    }
}
