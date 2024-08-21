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
    // 1번 자리 : 1 냉동기, 2 건조기, 3 발골기
    // 2번 자리 : 4 포장기, 5 젓갈 충전기, 6 전분 합성기, 7 통조림 제작기
    // 3번 자리 : 4 포장기, 8 튀김기
    // 4번 자리 : 4 포장기
    public int[][] facilityElements = new int[6][];

    // status of facilities. 0 if not installed, and line types below
    // 1 : 냉동, 2 : 말리기, 3 : 젓갈, 4 : 어묵, 5 : 통조림
    // ( 1400 냉동, 2400 말리기, 3540 젓갈, 3684 어묵, 3740 통조림)
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
