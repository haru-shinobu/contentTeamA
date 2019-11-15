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
    RectTransform EyeAbilityTear1;
    RectTransform EyeAbilityTear2;
    RectTransform EyeAbilityTear3;
    RectTransform EyeAbilityTear4;
    RayAbility Abilitycontroller;
    Vector2 StandardTearPos;

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
        col = transform.GetChild(11).gameObject.GetComponent<Image>();
        col.enabled = true;
        clockhand = GameObject.Find("ClockhandImage").GetComponent<RectTransform>();
        ResetFlag = GameObject.Find("GameMaster").GetComponent<GameStageSetting>().ResetStatus;
        clocker = GameObject.Find("ClockImage").GetComponent<RectTransform>();
        
        clockertop = GameObject.Find("clocktopImage").GetComponent<RectTransform>();
        pos = clocker.localPosition - clockertop.localPosition;
        handpos = clocker.localPosition - clockhand.localPosition;
        if (ResetFlag)
        {
            ResetFlag = !ResetFlag;
            Flag = true;
            col.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            ResetFlag = false;
            Flag = false;
            col.color = new Color32(255, 255, 255, 0);
        }
        
        StartPos = clocker.localPosition;
        CountTimer = 0;
        ResetTimer = 0;
        //Ability関係
        Abilitycontroller = GameObject.Find("GameMaster").GetComponent<RayAbility>();

        EyeAbilityNormal = GameObject.Find("ANImage").GetComponent<RectTransform>();
        EyeAbilityWarp = GameObject.Find("AWImage").GetComponent<RectTransform>();
        EyeAbilityStop = GameObject.Find("ASImage").GetComponent<RectTransform>();
        EyeAbilityExplosion = GameObject.Find("AEImage").GetComponent<RectTransform>();
        EyeAbilityReset = GameObject.Find("ARImage").GetComponent<RectTransform>();

        EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = true;
        EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false; 
        EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false; 
        EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
        EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;

        //AbilityMenu関係
        StandardTearPos = EyeAbilityNormal.transform.localPosition;
        EyeAbilityTear1 = GameObject.Find("MenuWindowImage (1)").GetComponent<RectTransform>();
        EyeAbilityTear2 = GameObject.Find("MenuWindowImage (2)").GetComponent<RectTransform>();
        EyeAbilityTear3 = GameObject.Find("MenuWindowImage (3)").GetComponent<RectTransform>();
        EyeAbilityTear4 = GameObject.Find("MenuWindowImage (4)").GetComponent<RectTransform>();
    }

    void Update()
    {

        if (Flag)//シーン読み込み直後
        {
            ResetTimer += Time.deltaTime;


            RayAbilityWhiteOut(ResetTimer, true, true);

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
                    Debug.Log(ResetTimer);
                    ResetTimer = 0;
                    Flag = !Flag;
                    RayAbilityWhiteOut(0, false, true);
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

        if (Abilitycontroller.AbilityMenuOpenFlag)
        {
            EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = true;
            EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = true;
            EyeAbilityStop.gameObject.GetComponent<Image>().enabled = true;
            EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = true;
            EyeAbilityReset.gameObject.GetComponent<Image>().enabled = true;
            EyeAbilityTear1.gameObject.GetComponent<Image>().enabled = true;
            EyeAbilityTear2.gameObject.GetComponent<Image>().enabled = true;
            EyeAbilityTear3.gameObject.GetComponent<Image>().enabled = true;
            EyeAbilityTear4.gameObject.GetComponent<Image>().enabled = true;
            float ANum = Time.deltaTime * 10;
            EyeAbilityTear1.localPosition = Vector2.Lerp(EyeAbilityTear1.localPosition, StandardTearPos + new Vector2(0, -110), ANum);
            EyeAbilityTear2.localPosition = Vector2.Lerp(EyeAbilityTear2.localPosition, StandardTearPos + new Vector2(0, -230), ANum);
            EyeAbilityTear3.localPosition = Vector2.Lerp(EyeAbilityTear3.localPosition, StandardTearPos + new Vector2(0, -350), ANum);
            EyeAbilityTear4.localPosition = Vector2.Lerp(EyeAbilityTear4.localPosition, StandardTearPos + new Vector2(0, -470), ANum);

            switch (Abilitycontroller.AbilityNow)
            {
                case 0://160//280//400//520
                    EyeAbilityNormal.localPosition    = StandardTearPos;
                    EyeAbilityWarp.localPosition      = Vector2.Lerp(EyeAbilityWarp.localPosition      , StandardTearPos + new Vector2(0, -120), ANum);
                    EyeAbilityStop.localPosition      = Vector2.Lerp(EyeAbilityStop.localPosition      , StandardTearPos + new Vector2(0, -240), ANum);
                    EyeAbilityExplosion.localPosition = Vector2.Lerp(EyeAbilityExplosion.localPosition , StandardTearPos + new Vector2(0, -360), ANum);
                    EyeAbilityReset.localPosition     = Vector2.Lerp(EyeAbilityReset.localPosition, StandardTearPos + new Vector2(0, -480), ANum);

                    break;
                case 1:
                    EyeAbilityNormal.localPosition    = Vector2.Lerp(EyeAbilityNormal.localPosition, StandardTearPos + new Vector2(0, -480), ANum);
                    EyeAbilityWarp.localPosition      = StandardTearPos; 
                    EyeAbilityStop.localPosition      = Vector2.Lerp(EyeAbilityStop.localPosition      , StandardTearPos + new Vector2(0, -120), ANum);
                    EyeAbilityExplosion.localPosition = Vector2.Lerp(EyeAbilityExplosion.localPosition , StandardTearPos + new Vector2(0, -240), ANum);
                    EyeAbilityReset.localPosition     = Vector2.Lerp(EyeAbilityReset.localPosition, StandardTearPos + new Vector2(0, -360), ANum);
                    
                    break;
                case 2:
                    EyeAbilityNormal.localPosition    = Vector2.Lerp(EyeAbilityNormal.localPosition   , StandardTearPos + new Vector2(0, -360), ANum);
                    EyeAbilityWarp.localPosition      = Vector2.Lerp(EyeAbilityWarp.localPosition, StandardTearPos + new Vector2(0, -480), ANum);
                    EyeAbilityStop.localPosition      = StandardTearPos; 
                    EyeAbilityExplosion.localPosition = Vector2.Lerp(EyeAbilityExplosion.localPosition , StandardTearPos + new Vector2(0, -120), ANum);
                    EyeAbilityReset.localPosition     = Vector2.Lerp(EyeAbilityReset.localPosition, StandardTearPos + new Vector2(0, -240), ANum);
                    
                    break;
                case 3:
                    EyeAbilityNormal.localPosition    = Vector2.Lerp(EyeAbilityNormal.localPosition   , StandardTearPos + new Vector2(0, -240), ANum);
                    EyeAbilityWarp.localPosition      = Vector2.Lerp(EyeAbilityWarp.localPosition     , StandardTearPos + new Vector2(0, -360), ANum);
                    EyeAbilityStop.localPosition      = Vector2.Lerp(EyeAbilityStop.localPosition, StandardTearPos + new Vector2(0, -480), ANum);
                    EyeAbilityExplosion.localPosition = StandardTearPos; 
                    EyeAbilityReset.localPosition     = Vector2.Lerp(EyeAbilityReset.localPosition, StandardTearPos + new Vector2(0, -120), ANum);
                    
                    break;
                case 4:
                    EyeAbilityNormal.localPosition    = Vector2.Lerp(EyeAbilityNormal.localPosition    , StandardTearPos + new Vector2(0, -120), ANum);
                    EyeAbilityWarp.localPosition      = Vector2.Lerp(EyeAbilityWarp.localPosition      , StandardTearPos + new Vector2(0, -240), ANum);
                    EyeAbilityStop.localPosition      = Vector2.Lerp(EyeAbilityStop.localPosition      , StandardTearPos + new Vector2(0, -360), ANum);
                    EyeAbilityExplosion.localPosition = Vector2.Lerp(EyeAbilityExplosion.localPosition, StandardTearPos + new Vector2(0, -480), ANum);
                    EyeAbilityReset.localPosition     = StandardTearPos;
                    
                    break;
            }
        }
        else
        {
            EyeAbilityNormal.localPosition = StandardTearPos;
            EyeAbilityWarp.localPosition = StandardTearPos;
            EyeAbilityStop.localPosition = StandardTearPos;
            EyeAbilityExplosion.localPosition = StandardTearPos;
            EyeAbilityReset.localPosition = StandardTearPos;
            EyeAbilityTear1.gameObject.GetComponent<Image>().enabled = false;
            EyeAbilityTear2.gameObject.GetComponent<Image>().enabled = false;
            EyeAbilityTear3.gameObject.GetComponent<Image>().enabled = false;
            EyeAbilityTear4.gameObject.GetComponent<Image>().enabled = false;
            EyeAbilityTear1.localPosition = StandardTearPos;
            EyeAbilityTear2.localPosition = StandardTearPos;
            EyeAbilityTear3.localPosition = StandardTearPos;
            EyeAbilityTear4.localPosition = StandardTearPos;
            switch (Abilitycontroller.AbilityNow)
            {
                case 0:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = true;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;
                    break;
                case 1:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = true;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;
                    break;
                case 2:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = true;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;
                    break;
                case 3:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = true;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;
                    break;
                case 4:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = true;
                    break;
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

    bool bFadeStart;
    bool bFadeOut;
    float SFadenum;
    float OFadenum;
    public void RayAbilityWhiteOut(float Count, bool bFadeC, bool bFadeO)
    {
        if (bFadeO)//restart
        {
            bFadeStart = bFadeC;
            SFadenum = Count;
        }
        else//AbilityState
        {
            OFadenum = Count;
            if (3 <= OFadenum) OFadenum = 3;
             bFadeOut = bFadeC;
        }


        if(bFadeStart)
            if(bFadeOut)
                if (42 * OFadenum <= (255 - SFadenum * 28))
                    col.color = new Color32(255, 255, 255, (byte)(255 - SFadenum * 28));
                else
                    col.color = new Color32(255, 255, 255, (byte)(42 * OFadenum));
            else
                col.color = new Color32(255, 255, 255, (byte)(255 - SFadenum * 28));
        else
            if (bFadeOut)
                col.color = new Color32(255, 255, 255, (byte)(42 * OFadenum));
            else
                col.color = new Color32(255, 255, 255, 0);
    }
}
