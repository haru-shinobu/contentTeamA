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
    public bool GameCrearLoadFlag;

    void Start()
    {
        this.Limittimer = gameObject.GetComponent<GameStageSetting>().NowStageTimeLimit;
        timerText = GameObject.Find("Timer");
        rect = timerText.GetComponent<RectTransform>();
        memo = timerText.GetComponent<Text>();
        TextSpace = rect.sizeDelta;
        timepos = rect.transform.localPosition;
        GameCrearLoadFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        float onetime = NowTime;

        if (!GameCrearLoadFlag)
        {
            rect.sizeDelta = TextSpace;
            rect.localPosition = timepos;
            timerText.GetComponent<Text>().text = ("TimeLimit") + ("\n") + (Limittimer).ToString("F0") + (" sec");
        }
        else
        {
            rect.sizeDelta = TextSpace;
            rect.localPosition = timepos;
            timerText.GetComponent<Text>().text = ("ClearTime") + ("\n") + (Limittimer - onetime).ToString("F0") + (" sec");
        }
        if (NowTime < 0)
            gameObject.GetComponent<GameStageSetting>().GAMEOVER();
    }
}
