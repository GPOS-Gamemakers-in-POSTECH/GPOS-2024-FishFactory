// dontdestroy object that save status of fish tanks and facilities installation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class installStatusManager : MonoBehaviour
{
    public static installStatusManager Instance { get; private set; }
    public int[] MinMulFishTank = new int[9];
    public int[] OceanFishTank = new int[20];
    public int[] IndoorFishTank = new int[9];

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
