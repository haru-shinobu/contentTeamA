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
    private bool GameOverFlag;
    private bool DoorPassFlag;
    void Start()
    {
        this.Limittimer = gameObject.GetComponent<GameStageSetting>().NowStageTimeLimit;
        timerText = GameObject.Find("Timer");
        rect = timerText.GetComponent<RectTransform>();
        memo = timerText.GetComponent<Text>();
        TextSpace = rect.sizeDelta;
        timepos = rect.transform.localPosition;
        GameCrearLoadFlag = false;
        GameOverFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        float onetime = NowTime;

        if (!GameCrearLoadFlag)
        {
            if (DoorPassFlag)
            {
                rect.sizeDelta = TextSpace;
                rect.localPosition = timepos;
                timerText.GetComponent<Text>().text = ("のこり時間") + ("\n") + (Limittimer).ToString("F0") + ("秒");
            }
            else
            {
                rect.sizeDelta = TextSpace;
                rect.localPosition = timepos;
                timerText.GetComponent<Text>().text = ("TimeLimit") + ("\n") + (Limittimer).ToString("F0") + (" sec");
            }
        }
        else
        {
            rect.sizeDelta = TextSpace;
            rect.localPosition = timepos;
            timerText.GetComponent<Text>().text = ("ClearTime") + ("\n") + (Limittimer - onetime).ToString("F0") + (" sec");
        }
        if (NowTime < 0)
            if (!GameOverFlag)
            {
                gameObject.GetComponent<GameStageSetting>().GAMEOVER();
                GameOverFlag = true;
            }
    }
}
