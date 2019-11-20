using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-2)]//スクリプト実行順
public class GameTimerDirector : MonoBehaviour
{
    GameObject timerText;
    RectTransform rect;
    float Limittimer;
    Text memo;
    Vector3 timepos;
    Vector2 TextSpace;
    public float NowTime;

    void Start()
    {
        this.Limittimer = gameObject.GetComponent<GameStageSetting>().NowStageTimeLimit;
        timerText = GameObject.Find("Timer");
        rect = timerText.GetComponent<RectTransform>();
        memo = timerText.GetComponent<Text>();
        TextSpace = rect.sizeDelta;
        timepos = rect.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float onetime = NowTime;

        rect.sizeDelta = TextSpace;
        rect.localPosition = timepos;
        timerText.GetComponent<Text>().text = ("TimeLimit") + ("\n") + (Limittimer/* - onetime*/).ToString("F0")+(" min");
    }
}
