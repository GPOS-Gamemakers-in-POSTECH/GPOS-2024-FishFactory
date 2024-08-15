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

    // Update is called once per frame
    void Update()
    {
        
    }

    void gameStart()
    {
        SceneManager.LoadScene("MinMul");
    }

    void gameClose()
    {
                // 에디터에서 테스트할 때 종료
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // 빌드된 게임에서 종료
                Application.Quit();
        #endif
    }
}
