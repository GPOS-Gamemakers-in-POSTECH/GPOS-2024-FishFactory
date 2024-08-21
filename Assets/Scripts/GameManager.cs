using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isInteracting = false; // Check If Player is Interacting
    public bool[] freshFishTank = new bool[9];
    public bool[] oceanFishTank = new bool[20];
    public bool[] indoorFishTank = new bool[9];

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
}
