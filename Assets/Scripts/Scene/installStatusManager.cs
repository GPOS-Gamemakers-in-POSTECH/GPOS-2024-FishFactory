// dontdestroy object that save status of fish tanks and facilities installation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class installStatusManager : MonoBehaviour
{
    public static installStatusManager Instance { get; private set; }

    public bool[] MinMulFishTank = new bool[9];
    public bool[] OceanFishTank = new bool[20];
    public bool[] IndoorFishTank = new bool[9];




    // status of facility elements. each line has 4 elements. 0 if not installed, and
    // 1�� �ڸ� : 1 �õ���, 2 ������, 3 �߰��
    // 2�� �ڸ� : 4 �����, 5 ���� ������, 6 ���� �ռ���, 7 ������ ���۱�
    // 3�� �ڸ� : 4 �����, 8 Ƣ���
    // 4�� �ڸ� : 4 �����
    public int[][] facilityElements = new int[6][];

    // status of facilities. 0 if not installed, and line types below
    // 1 : freeze, 2 : dry, 3 : jutgal, 4 : fish cake, 5 : can
    // ( 1400 �õ�, 2400 ������, 3540 ����, 3684 �, 3740 ������)
    public int[] facilityLine = new int[6];

    public int[] isFacilityWorking = new int[6];

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

        for (int i = 0; i < facilityElements.Length; i++)
        {
            facilityElements[i] = new int[4];
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SleepScene")
        {
            for (int i=0; i<6; i++)
            {
                if (isFacilityWorking[i]==1)
                {
                    isFacilityWorking[i] = 2;
                }
            }
        }
    }


}
