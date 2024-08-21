// dontdestroy object that save status of fish tanks and facilities installation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class installStatusManager : MonoBehaviour
{
    public static installStatusManager Instance { get; private set; }
    
    // status of fish tanks. 0 if not installed, 1 if installed
    public int[] MinMulFishTank = new int[9];
    public int[] OceanFishTank = new int[20];
    public int[] IndoorFishTank = new int[9];



    // status of facility elements. each line has 4 elements. 0 if not installed, and
    // 1�� �ڸ� : 1 �õ���, 2 ������, 3 �߰��
    // 2�� �ڸ� : 4 �����, 5 ���� ������, 6 ���� �ռ���, 7 ������ ���۱�
    // 3�� �ڸ� : 4 �����, 8 Ƣ���
    // 4�� �ڸ� : 4 �����
    public int[][] facilityElements = new int[6][];

    // status of facilities. 0 if not installed, and line types below
    // 1 : �õ�, 2 : ������, 3 : ����, 4 : �, 5 : ������
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


}
