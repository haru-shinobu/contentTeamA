using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3TimeManager : MonoBehaviour
{
    public static int ClearTime = 0;
    float LimitedTime = 0;
    void Start()
    {
        ClearTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if文などでフェード終了とかから
         * 計測できるようにするとか
         * したほうがいいかも？
         * あるいはプレイヤーの初期操作以降…
         */
        ClearTime = (int)Time.time;
        if (LimitedTime < ClearTime)
            Debug.Log("GameOver");
    }
    public static void PenaltyTime(int time)
    {
        ClearTime -= time;
    }
    public void LimitTime(int time)
    {
        LimitedTime = time;
    }
    public static int GetTime()
    {
        return ClearTime;
    }
    public static void ResetTime()
    {
        ClearTime = 0;
    }
}

//Time.timeScale = 0.1f;
//でゲーム全体がスローになるらしい
