using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(3)]//スクリプト実行順
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

    
    ParticleSystem clockside;
    Vector3 StartPos;
    Vector3 pos, handpos;
    float CountTimer;
    float ResetTimer;
    bool Flag;
    bool ResetFindFlag;
    GameObject GameMaster;
    GameTimerDirector GNTimer;
    bool GameStartFlag;
    static public bool ResetFlag = false;
    static public bool GameProgressionFlag = true;
    float RestSecond;
    Color colers;
    //text
    Text AbilityText;

    //Ability毎の光用
    RayAbility RAy;
    GameObject[] warpObjects;
    GameObject[] stopObjects;
    GameObject[] stopObjects2;
    GameObject[] stopObjects3;
    GameObject[] breakObjects;
    public Texture texture;
    public Texture texture2;

    //道開きメッセージ用
    public bool OpenRootFlag;
    ParticleSystem ClockParticle;
    void Start()
    {
        ClockParticle = gameObject.transform.GetChild(13).GetChild(0).GetComponent<ParticleSystem>();
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        GameMaster = GameObject.Find("GameMaster");
        GNTimer = GameMaster.GetComponent<GameTimerDirector>();
        //clock関係
        col = transform.GetChild(12).gameObject.GetComponent<Image>();//ホワイトアウト用
        col.enabled = true;
        clockhand = GameObject.Find("ClockhandImage").GetComponent<RectTransform>();
        clocker = GameObject.Find("ClockImage").GetComponent<RectTransform>();

        clockertop = GameObject.Find("clocktopImage").GetComponent<RectTransform>();
        pos = clocker.localPosition - clockertop.localPosition;
        handpos = clocker.localPosition - clockhand.localPosition;
        RestSecond = GameMaster.GetComponent<GameStageSetting>().NowStageTimeLimit;
        clockhand.eulerAngles = new Vector3(0, 0, 180 + 6 * RestSecond);

        StartPos = clocker.localPosition;
        CountTimer = 0;
        ResetTimer = 0;
        //Ability関係
        Abilitycontroller = GameMaster.GetComponent<RayAbility>();

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

        ResetFlag = GameMaster.GetComponent<GameStageSetting>().ResetStatus;

        GameStartFlag = false;
        if (ResetFlag)
        {
            ResetFlag = !ResetFlag;
            Flag = true;
            col.enabled = true;
            col.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            ResetFlag = false;
            Flag = false;
            col.color = new Color32(255, 255, 255, 0);
            col.enabled = false;
        }
        //Ability Text
        AbilityText = GameObject.Find("AbilityText").GetComponent<Text>();
        
        //AbilityLiting
        RAy = GameMaster.GetComponent<RayAbility>();
        TagnameMemory();
    }
    //タイム処理
    void FixedUpdate()
    {
        int Second = (int)GNTimer.NowTime;
        if (Second == 60)
            ClockParticle.startColor = Color.red;
        
        
        if (Flag)//シーン読み込み直後
        {
            ResetTimer += Time.deltaTime;

            RayAbilityWhiteOut(ResetTimer, true, true);
            if (ResetTimer < 2.5f)//2.5秒間画面中央で針回し
                CallResetScene();
            else
            {
                clocker.localPosition = Vector3.Lerp(clocker.localPosition, StartPos, Time.deltaTime);
                clockertop.localPosition = clocker.localPosition - pos;
                clockhand.localPosition = clocker.localPosition - handpos;
                clockhand.localEulerAngles = new Vector3(0, 0, 180 - 6 * (-Second));
                if (StartPos.x - clocker.localPosition.x < 1f)
                {
                    ResetTimer = 0;
                    Flag = !Flag;
                    RayAbilityWhiteOut(0, false, true);
                    col.enabled = false;

                    clocker.localPosition =  StartPos;
                    clockertop.localPosition = clocker.localPosition - pos;
                    clockhand.localPosition = clocker.localPosition - handpos;
                }
            }
        }
        else//通常
        {
            CountTimer += Time.deltaTime;
            if (CountTimer >= 1)
            {
                CountTimer -= 1.0f;                
                if (RestSecond - Second >= 0)
                    clockhand.localEulerAngles = new Vector3(0, 0, 180 - 6 * Second);
            }
        }
    }

    void Update() {

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
            EyeAbilityTear4.localPosition = Vector2.Lerp(EyeAbilityTear4.localPosition, StandardTearPos + new Vector2(0, -200), ANum);
            EyeAbilityTear3.localPosition = Vector2.Lerp(EyeAbilityTear3.localPosition, StandardTearPos + new Vector2(0, -415), ANum);
            EyeAbilityTear2.localPosition = Vector2.Lerp(EyeAbilityTear2.localPosition, StandardTearPos + new Vector2(0, -630), ANum);
            EyeAbilityTear1.localPosition = Vector2.Lerp(EyeAbilityTear1.localPosition, StandardTearPos + new Vector2(0, -845), ANum);

            switch (Abilitycontroller.AbilityNow)
            {
                case 0://160//280//400//520
                    EyeAbilityNormal.localPosition = StandardTearPos;
                    EyeAbilityWarp.localPosition = Vector2.Lerp(EyeAbilityWarp.localPosition, StandardTearPos + new Vector2(0, -230), ANum);
                    EyeAbilityStop.localPosition = Vector2.Lerp(EyeAbilityStop.localPosition, StandardTearPos + new Vector2(0, -445), ANum);
                    EyeAbilityExplosion.localPosition = Vector2.Lerp(EyeAbilityExplosion.localPosition, StandardTearPos + new Vector2(0, -660), ANum);
                    EyeAbilityReset.localPosition = Vector2.Lerp(EyeAbilityReset.localPosition, StandardTearPos + new Vector2(0, -875), ANum);
                    AbilityText.text = ("魔眼を使うときは動けなくなる。");
                    break;
                case 1:
                    EyeAbilityNormal.localPosition = Vector2.Lerp(EyeAbilityNormal.localPosition, StandardTearPos + new Vector2(0, -875), ANum);
                    EyeAbilityWarp.localPosition = StandardTearPos;
                    EyeAbilityStop.localPosition = Vector2.Lerp(EyeAbilityStop.localPosition, StandardTearPos + new Vector2(0, -230), ANum);
                    EyeAbilityExplosion.localPosition = Vector2.Lerp(EyeAbilityExplosion.localPosition, StandardTearPos + new Vector2(0, -445), ANum);
                    EyeAbilityReset.localPosition = Vector2.Lerp(EyeAbilityReset.localPosition, StandardTearPos + new Vector2(0, -660), ANum);
                    AbilityText.text = ("転移の魔眼。とある物を見つめ続けたらEで跳ぶことができる");
                    break;
                case 2:
                    EyeAbilityNormal.localPosition = Vector2.Lerp(EyeAbilityNormal.localPosition, StandardTearPos + new Vector2(0, -660), ANum);
                    EyeAbilityWarp.localPosition = Vector2.Lerp(EyeAbilityWarp.localPosition, StandardTearPos + new Vector2(0, -875), ANum);
                    EyeAbilityStop.localPosition = StandardTearPos;
                    EyeAbilityExplosion.localPosition = Vector2.Lerp(EyeAbilityExplosion.localPosition, StandardTearPos + new Vector2(0, -230), ANum);
                    EyeAbilityReset.localPosition = Vector2.Lerp(EyeAbilityReset.localPosition, StandardTearPos + new Vector2(0, -445), ANum);
                    AbilityText.text = ("停止の魔眼。見つめ続けた動くものをEで止められるが、解除しないと次が使えない");
                    break;
                case 3:
                    EyeAbilityNormal.localPosition = Vector2.Lerp(EyeAbilityNormal.localPosition, StandardTearPos + new Vector2(0, -445), ANum);
                    EyeAbilityWarp.localPosition = Vector2.Lerp(EyeAbilityWarp.localPosition, StandardTearPos + new Vector2(0, -660), ANum);
                    EyeAbilityStop.localPosition = Vector2.Lerp(EyeAbilityStop.localPosition, StandardTearPos + new Vector2(0, -875), ANum);
                    EyeAbilityExplosion.localPosition = StandardTearPos;
                    EyeAbilityReset.localPosition = Vector2.Lerp(EyeAbilityReset.localPosition, StandardTearPos + new Vector2(0, -230), ANum);
                    AbilityText.text = ("破壊の魔眼。見つめ続けたひび割れた物体をEで破壊できる");
                    break;
                case 4:
                    EyeAbilityNormal.localPosition = Vector2.Lerp(EyeAbilityNormal.localPosition, StandardTearPos + new Vector2(0, -230), ANum);
                    EyeAbilityWarp.localPosition = Vector2.Lerp(EyeAbilityWarp.localPosition, StandardTearPos + new Vector2(0, -445), ANum);
                    EyeAbilityStop.localPosition = Vector2.Lerp(EyeAbilityStop.localPosition, StandardTearPos + new Vector2(0, -660), ANum);
                    EyeAbilityExplosion.localPosition = Vector2.Lerp(EyeAbilityExplosion.localPosition, StandardTearPos + new Vector2(0, -875), ANum);
                    EyeAbilityReset.localPosition = StandardTearPos;
                    AbilityText.text = ("初見の魔眼。力(E)を溜めると使えるリセット能力。");
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
            EyeAbilityTear4.localPosition = StandardTearPos;
            EyeAbilityTear3.localPosition = StandardTearPos;
            EyeAbilityTear2.localPosition = StandardTearPos;
            EyeAbilityTear1.localPosition = StandardTearPos;
            AbilityText.fontSize = 50;
            switch (Abilitycontroller.AbilityNow)
            {
                case 0:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = true;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;
                    AbilityText.text = ("Qで魔眼を使う。目に集中するので動けない");
                    if (OpenRootFlag)
                        AbilityText.text = ("風の音がする…どこかに道が開いたようだ");
                    break;
                case 1:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = true;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;
                    AbilityText.text = ("転移の魔眼。とある物を見つめ続けたらEで跳ぶことができる");
                    break;
                case 2:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = true;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;
                    AbilityText.text = ("停止の魔眼。見つめ続けた動くものをEで止められるが、解除しないと次が使えない");
                    break;
                case 3:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = true;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = false;
                    AbilityText.text = ("破壊の魔眼。見つめ続けたひび割れた物体をEで破壊できる");
                    break;
                case 4:
                    EyeAbilityNormal.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityWarp.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityStop.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityExplosion.gameObject.GetComponent<Image>().enabled = false;
                    EyeAbilityReset.gameObject.GetComponent<Image>().enabled = true;
                    AbilityText.text = ("初見の魔眼。力(E)を溜めると使えるリセット能力。");
                    break;
            }

            if (!GameStartFlag)
            {
                CountTimer = Time.time;
                AbilityText.text = ("制限時間までに出口を探せ！");
                if (4 < CountTimer)//***********************************************注意
                    GameStartFlag = true;
            }
        }
        Tagname(Abilitycontroller.AbilityNow);
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
        col.enabled = true;
        if (!bFadeC && !bFadeO)
            col.enabled = false;
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

        if (bFadeStart)
        {
            if (bFadeOut)
            {
                if (42 * OFadenum <= (255 - SFadenum * 28))
                {
                    col.color = new Color32(255, 255, 255, (byte)(255 - SFadenum * 28));
                }
                else
                    col.color = new Color32(255, 255, 255, (byte)(42 * OFadenum));
            }
            else
                col.color = new Color32(255, 255, 255, (byte)(255 - SFadenum * 28));
        }
        else
            if (bFadeOut)
        {
            col.color = new Color32(255, 255, 255, (byte)(42 * OFadenum));   
        }
        else
        {
            col.color = new Color32(255, 255, 255, 0);
        }
    }
    //扉潜るたびに呼び出す。
    public void TagnameMemory()
    {
        /*GameObject[]*/ warpObjects  = GameObject.FindGameObjectsWithTag("WarpPoint");
        /*GameObject[]*/ stopObjects  = GameObject.FindGameObjectsWithTag("move&Stop");
        /*GameObject[]*/ stopObjects2 = GameObject.FindGameObjectsWithTag("Floor&Stop");
        /*GameObject[]*/ breakObjects = GameObject.FindGameObjectsWithTag("Break&Wall");
        /*GameObject[]*/ stopObjects3 = GameObject.FindGameObjectsWithTag("SpiralSteps");
        foreach (GameObject gameObj in warpObjects)
            gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));//r, g, b
        foreach (GameObject gameObj in stopObjects)
            gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));
        foreach (GameObject gameObj in stopObjects2)
            if (gameObj.GetComponent<MeshRenderer>())
                gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));
        foreach (GameObject gameObj in breakObjects)
            if (gameObj.GetComponent<MeshRenderer>())
                gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));
            else
                gameObj.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));
        foreach (GameObject gameObj in stopObjects3)
        {
            gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));
            gameObj.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture2); 
        }
    }
    //能力選択時の光る枠をセット、アクティブ
    void Tagname(int Anow)
    {
        bool StopFlag = RAy.AbilityStopEmissionFlag;
        if (Anow == 1)
        {
            foreach (GameObject gameObj in warpObjects)
            {
                Emission(gameObj);
            }
        }
        else
        {
            foreach (GameObject gameObj in warpObjects)
            {
                gameObj.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
        }
        if (!StopFlag)
        {
            if (Anow == 2)
            {
                foreach (GameObject gameObj in stopObjects)
                {
                    gameObj.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture2);
                    gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));
                    Emission(gameObj);
                }
                foreach (GameObject gameObj in stopObjects2)
                {
                    if (gameObj)
                    {
                        if (gameObj.transform.name == "PendulumPoleShaft")
                            gameObj.transform.GetComponent<PendulumShaft>().StopTexChange(texture2, true, new Color(0, 0, 1));
                        if (gameObj.GetComponent<MeshRenderer>())
                        {
                            gameObj.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture2);
                            gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));
                        }
                        Emission(gameObj);
                    }
                }
                foreach (GameObject gameObj in stopObjects3)
                {
                    gameObj.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture2);
                    gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 1));
                    Emission(gameObj);
                }
            }
            else
            {
                foreach (GameObject gameObj in stopObjects)
                {
                    gameObj.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
                foreach (GameObject gameObj in stopObjects2)
                {
                    if (gameObj)
                    {
                        if (gameObj.transform.name == "PendulumPoleShaft")
                            gameObj.transform.GetComponent<PendulumShaft>().StopTexChange(texture2, false, new Color(0, 0, 1));
                        if(gameObj.GetComponent<MeshRenderer>())
                        gameObj.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                    }
                }
                foreach (GameObject gameObj in stopObjects3)
                {
                    gameObj.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
            }
        }
        else
        {
            string stopname = RAy.StopObjectName;
            
            foreach (GameObject gameObj in stopObjects)
            {
                if (gameObj.transform.root.name != stopname)
                {
                    gameObj.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
                else
                {
                    gameObj.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture);
                    gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0.06542563f));
                    gameObj.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                }
            }
            foreach (GameObject gameObj in stopObjects2)
            {
                if (gameObj)
                {
                    if (gameObj.transform.root.name != stopname)
                    {//
                        if (gameObj.transform.name == "PendulumPoleShaft")
                            gameObj.transform.GetComponent<PendulumShaft>().StopTexChange(texture2, false, new Color(0, 1, 0.06542563f));
                        if (gameObj.GetComponent<MeshRenderer>())
                            gameObj.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                    }
                    else
                    {
                        gameObj.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture);
                        gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0.06542563f));
                        gameObj.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                        if (gameObj.transform.name == "PendulumPoleShaft")
                            gameObj.transform.GetComponent<PendulumShaft>().StopTexChange(texture, true, new Color(0, 1, 0.06542563f));
                    }
                }
            }
            //*****
            foreach (GameObject gameObj in stopObjects3)
            {
                if (gameObj.transform.root.name != stopname)
                {
                    gameObj.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
                else
                {
                    gameObj.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture);
                    gameObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0.06542563f));
                    gameObj.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                }
            }
        }
        if (Anow==3)
        {
            foreach (GameObject gameObj in breakObjects)
            {
                Emission(gameObj);
            }
        }
        else
        {
            foreach (GameObject gameObj in breakObjects)
            {
                if (gameObj.GetComponent<MeshRenderer>())
                    gameObj.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                else
                    gameObj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
        }
    }
    void Emission(GameObject gObj)
    {
        if (gObj.GetComponent<MeshRenderer>())
            gObj.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        else
            if (gObj.GetComponent<MeshRenderer>())
            gObj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }
}
