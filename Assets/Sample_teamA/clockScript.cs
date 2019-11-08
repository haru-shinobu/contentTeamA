using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clockScript : MonoBehaviour
{
    Image col;   

    RectTransform clocker;
    RectTransform clockertop;
    RectTransform clockhand;
    RectTransform EyeAbilityNormal;
    RectTransform EyeAbilityWarp;
    RectTransform EyeAbilityStop;
    RectTransform EyeAbilityExplosion;
    RectTransform EyeAbilityReset;

    GameObject clockParticleBlock;
    ParticleSystem clockside;
    Vector3 StartPos;
    Vector3 pos ,handpos;
    float CountTimer;
    float ResetTimer;
    bool Flag;
    static public bool ResetFlag = false;
    void Start()
    {
        //clock関係
        col = transform.GetChild(1).gameObject.GetComponent<Image>();

        clockhand = GameObject.Find("ClockhandImage").GetComponent<RectTransform>();
        Flag = false;
        ResetFlag = GameObject.Find("GameMaster").GetComponent<GameStageSetting>().ResetStatus;
        clocker = GameObject.Find("ClockImage").GetComponent<RectTransform>();

        clockertop = GameObject.Find("clocktopImage").GetComponent<RectTransform>();
        pos = clocker.localPosition - clockertop.localPosition;
        handpos = clocker.localPosition - clockhand.localPosition;
        if (ResetFlag)
        {
            ResetFlag = !ResetFlag;
            Flag = true;
        }
        else
        {
            col.enabled = false;
        }
        StartPos = clocker.localPosition;
        CountTimer = 0;
        ResetTimer = 0;
        //Ability関係
        EyeAbilityNormal = GameObject.Find("AImage").GetComponent<RectTransform>();
        EyeAbilityWarp = GameObject.Find("AWImage").GetComponent<RectTransform>();
        EyeAbilityStop = GameObject.Find("ASImage").GetComponent<RectTransform>();
        EyeAbilityExplosion = GameObject.Find("AEImage").GetComponent<RectTransform>();
        EyeAbilityReset = GameObject.Find("ARImage").GetComponent<RectTransform>();
    }
    
    void Update()
    {
     
        if (Flag)//シーン読み込み直後
        {
            col.color = new Color32(255, 255, 255, (byte)(255 - ResetTimer * 28));
            ResetTimer += Time.deltaTime;
            if (ResetTimer < 3)//3秒間画面中央で針回し
                CallResetScene();
            else if (3 <= ResetTimer && ResetTimer < 3.2f)
            {//針を頂点へ向けてる
                clockhand.eulerAngles = new Vector3(0, 0, 180);
            }
            else
            {
                clocker.localPosition = Vector3.Lerp(clocker.localPosition, StartPos, Time.deltaTime);
                clockertop.localPosition = clocker.localPosition - pos;
                clockhand.localPosition = clocker.localPosition - handpos;
                if (StartPos.x - clocker.localPosition.x < 1f)
                {
                    ResetTimer = 0;
                    Flag = !Flag;
                    col.enabled = false;
                }
            }
        }
        else//通常
        {
            CountTimer += Time.deltaTime;
            if (CountTimer > 1)
            {
                CountTimer -= 1.0f;
                clockhand.Rotate(new Vector3(0, 0, -6));
            }
        }
        

    }

    void CallResetScene()
    {
        clocker.localPosition = new Vector3(0, 0, 0);
        clockertop.localPosition = clocker.localPosition - pos;
        clockhand.localPosition = clocker.localPosition - handpos;
        clockhand.Rotate(new Vector3(0, 0, 12));
    }
}
