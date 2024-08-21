// dontdestroy object that save status of fish tanks and facilities installation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class installStatusManager : MonoBehaviour
{
    public static installStatusManager Instance { get; private set; }
    public bool[] MinMulFishTank = new bool[9];
    public bool[] OceanFishTank = new bool[20];
    public bool[] IndoorFishTank = new bool[9];

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
