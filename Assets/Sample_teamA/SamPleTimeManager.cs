using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamPleTimeManager : MonoBehaviour
{
    public static int ClearTime = 0;
    float LimitedTime = 0;
    void Start()
    {
        ClearTime = 0;
    }
    
    void Update()
    {
        /*if文などでフェード終了とかから
         * 計測できるようにするとか
         * したほうがいいかも？
         * あるいはプレイヤーの初期操作以降…
         */
         //クリアから時間停止処理及び、タイムオーバーの時の時間リセットしなければ・・・

        ClearTime = (int)Time.time;
        if (LimitedTime < ClearTime)
            Debug.Log("GameOver");
        //****この式を追加して表示に使用
        gameObject.transform.GetComponent<GameTimerDirector>().NowTime = ClearTime;
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

//Time.timeScale = 0.1f;
//でゲーム全体がスローになるらしい

}
