using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTimeManager : MonoBehaviour
{
    int ScoreTime;

    // Start is called before the first frame update
    void Start()
    {
        ScoreTime = 0;
        int t1 = Stage1TimeManager.GetTime();
        int t2 = Stage2TimeManager.GetTime();
        int t3 = Stage3TimeManager.GetTime();
        ScoreTime = t1 + t2 + t3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResultTime()
    {
        Debug.Log(ScoreTime);
    }
}
