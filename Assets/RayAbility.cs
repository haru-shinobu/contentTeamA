using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-4)]//スクリプト実行順
public class RayAbility : MonoBehaviour
{
    bool MouseControl;

    //音声ファイル格納用変数
    AudioSource audioSource;
    //audioSource = GetComponent<AudioSource>();
    //audioSource.PlayOneShot(stopSE);
    
    public AudioClip tameSE;
    public AudioClip worpSE;
    public AudioClip stopSE;
    public AudioClip hakaiSE;
    public AudioClip kirikaeSE;

    
    public GameObject Player;
    public GameObject CircleWarp;  //チャージカーソル用
    public GameObject CircleStop;  //チャージカーソル用
    public GameObject CircleBreak; //チャージカーソル用
    //public GameObject CoolTimeCircle;//クールタイム表示用
    Image coolcircle;

    public GameObject reticleWarp;  //カーソル用
    public GameObject reticleStop;  //カーソル用
    public GameObject reticleBreak; //カーソル用

    public int BreakAbilityPenaltyTime;//ペナルティ用
    public int ResetAbilityPenaltyTime;//ペナルティ用

    clockScript col;//リセット能力の視界濁らせる用

    public GameObject exploision;
    public GameObject CFX2_PickupDiamond2;
    public GameObject CFX_Explosion_B_Smoke;
    public GameObject WarpLoad;
    public GameObject WarpGeat;
    CursorColtroll carsor;

    public float AbilityChengeMenuTime; //メニュー表示用。
    public int AbilityNum;              //能力いくつもってるか
    float KeyQTime = 0;                 //能力メニュー開くための長押し判定用
    public bool AbilityMenuOpenFlag = false;    //能力メニュー開示判定用。あとで画像とかで表示するようにしないと…
    Vector3 center;                     //視線をとるため
    bool AbilityFlag;                   //能力行使中か否か
    float AbilityTriggerTime = 0;       //各種能力発動までのカウント用
    public string StopObjectName;    //Stop能力で別オブジェクトが反応しないようにするため
    public int AbilityNow;              //現在発動中の能力0～4
    public float BreakEyeCoolTime;
    float NextUseTime = 0;//クールタイム用
    bool MouseButtonFlag;
    bool Ability4KEYFlag;
    bool WarpParticleFlag;
    GameObject Instance;
    GameObject InstanceGeat;

    public bool EyeLongFlag;//アイテム取ったときレイを伸ばす。

    public bool AbilityStopEmissionFlag;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        MouseControl = GameObject.Find("GameMaster").GetComponent<GameStageSetting>().MouseMode;
        CFX2_PickupDiamond2 = (GameObject)Resources.Load("CFX2_PickupDiamond2");
        CFX_Explosion_B_Smoke = (GameObject)Resources.Load("CFX_Explosion_B_Smoke+Text");
        col = GameObject.Find("UICanvas").GetComponent<clockScript>();
        carsor = gameObject.transform.GetComponent<CursorColtroll>();
        center = new Vector3(Screen.width / 2, Screen.height / 2);
        Player = GameObject.Find("Player");
        AbilityNow = 0;
        StopObjectName = null;
        CircleWarp  = GameObject.Find("WarpCircleImage");
        CircleStop  = GameObject.Find("StopCircleImage");
        CircleBreak = GameObject.Find("BreakCircleImage");
        reticleWarp = GameObject.Find("WarpImage");
        reticleStop = GameObject.Find("StopImage");
        reticleBreak = GameObject.Find("BreakImage");
        WarpParticleFlag = false;
        AbilityMenuOpenFlag = false;
        AbilityStopEmissionFlag = false;
        EyeLongFlag = false;
        KeyQTime = 0;
        MouseButtonFlag = false;
        Ability4KEYFlag = false;
        coolcircle = /*CoolTimeCircle*/GameObject.Find("Rechage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        AbilitycodeQ();
        AbilityAction();
    }

    //Ability選択
    void AbilitycodeQ()
    {
        //クールタイム用
        if (0 < NextUseTime)
            NextUseTime -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Q))
            KeyQTime = 0;
        else
        if (Input.GetMouseButtonDown(0))
        {
            MouseButtonFlag = true;
            KeyQTime = 0;
        }
        //audioSource.PlayOneShot(kirikaeSE);

        if (MouseButtonFlag || Input.GetKey(KeyCode.Q))
            KeyQTime += Time.deltaTime;

        if (AbilityChengeMenuTime < KeyQTime)//MenuOpenしてるときの処理
        {
            AbilityMenuOpenFlag = true;//MenuOpenフラグ
            if (!MouseControl)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    AbilityNow--;
                    if (0 < NextUseTime)
                        if (AbilityNow == 3)
                            AbilityNow--;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    AbilityNow++;
                    if (0 < NextUseTime)
                        if (AbilityNow == 3)
                            AbilityNow++;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    AbilityNow--;
                    if (0 < NextUseTime)
                        if (AbilityNow == 3)
                            AbilityNow--;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    AbilityNow++;
                    if (0 < NextUseTime)
                        if (AbilityNow == 3)
                            AbilityNow++;
                }
            }

        }
        else
        if (!Input.anyKey && KeyQTime != 0)
        {
            KeyQTime = 0;
            AbilityNow++;
            if (0 < NextUseTime)
                if (AbilityNow == 3)
                    AbilityNow++;
        }

        //能力選択ループ用
        if (AbilityNow > AbilityNum) AbilityNow = 0;
        if (AbilityNow < 0) AbilityNow = AbilityNum;


        if (Input.GetKeyUp(KeyCode.Q) || Input.GetMouseButtonUp(0))
        {
            KeyQTime = 0;
            AbilityTriggerTime = 0;
            AbilityMenuOpenFlag = false;//MenuCloseフラグ
            MouseButtonFlag = false;
        }

        if (AbilityNow != 0)
        {
            Player.GetComponent<PlayerController>().PlayerAbility = true;
        }
        else
        {
            Player.GetComponent<PlayerController>().PlayerAbility = false;
        }
    }
    //Rayを飛ばしてる部分
    void AbilityAction()
    {
        Charge(AbilityTriggerTime);

        //Ability4(リセット)
        if (AbilityNow == 4)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)) Ability4KEYFlag = true;
            if(Ability4KEYFlag)
            {
                AbilityTriggerTime += Time.deltaTime;   
                col.RayAbilityWhiteOut(AbilityTriggerTime, true, false);
            }
            if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                Ability4KEYFlag = false;
                if (3 <= AbilityTriggerTime)
                {
                    //AbilityPenalty(ResetAbilityPenaltyTime);
                    Player.GetComponent<PlayerController>().ResetFlag = true;
                }
                else
                {
                    AbilityTriggerTime = 0;
                    col.RayAbilityWhiteOut(0, false, false);
                }
            }
        }
        //Ability0～3
        else
        {
            col.RayAbilityWhiteOut(0,false, false);
            Ray ray = Camera.main.ScreenPointToRay(center);
            RaycastHit hit;
            float raylong;
            if (0 == AbilityNow)
            {
                raylong = Player.transform.localScale.x * 2;
            }
            else
            {
                if (!EyeLongFlag)
                    raylong = 1000f;
                else
                    raylong = 1500;
                carsor.handflag = false;
            }

            if (Physics.Raycast(ray, out hit, raylong))
            {                
                reticleAction(hit.normal, hit.point);
                if (AbilityNow != 1 && WarpParticleFlag)
                {
                    Destroy(Instance);
                    Destroy(InstanceGeat);
                }
                switch (AbilityNow)
                {
                    case 0:
                        {
                            if (hit.collider.gameObject.tag == "Switch")
                            {
                                carsor.handflag = true;
                                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                {
                                    hit.collider.gameObject.SendMessage("TriggerOn");
                                    AbilityNow = 0;
                                }
                            }
                            else
                            {
                                carsor.handflag = false;
                            }
                        }
                        break;
                    case 1: //warp
                        {
                            if (hit.collider.gameObject.tag == "WarpPoint")
                            {
                                AbilityTriggerTime += Time.deltaTime;
                                if (!WarpParticleFlag)
                                {
                                    Instance = Instantiate(WarpLoad, Player.transform.position + new Vector3(0, Player.transform.localScale.y * 0.4f, 0), Camera.main.transform.rotation);
                                    InstanceGeat = Instantiate(WarpGeat, hit.point - Camera.main.transform.forward * 20, Camera.main.transform.rotation);
                                    WarpParticleFlag = true;
                                }

                                if (3 <= AbilityTriggerTime)
                                {
                                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                    {
                                        audioSource.PlayOneShot(worpSE);
                                        Player.transform.position = hit.point + (hit.normal * Player.transform.localScale.x);
                                        AbilityNow = 0;
                                    }
                                }
                                else
                                {
                                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                        AbilityTriggerTime = 0;
                                }
                            }
                            else
                            {
                                AbilityTriggerTime = 0;
                                if (WarpParticleFlag)
                                {
                                    WarpParticleFlag = false;
                                    Destroy(Instance);
                                    Destroy(InstanceGeat);
                                }
                            }
                        }
                        break;
                    case 2: //stop
                        {
                            if (StopObjectName == hit.collider.gameObject.transform.root.name || StopObjectName == null)
                            {
                                if (hit.collider.gameObject.tag == "move&Stop")
                                {
                                    AbilityTriggerTime += Time.deltaTime;
                                    if (3 <= AbilityTriggerTime)
                                    {
                                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                        {
                                            hit.collider.gameObject.SendMessage("CollStop");
                                            Instantiate(CFX2_PickupDiamond2, hit.point + (hit.normal * 3), Quaternion.identity);
                                            audioSource.PlayOneShot(stopSE);
                                            if (StopObjectName == null)
                                            {
                                                StopObjectName = hit.collider.gameObject.transform.root.name;
                                               
                                                AbilityStopEmissionFlag = true;
                                            }
                                            else
                                            {
                                                StopObjectName = null;
                                               
                                                AbilityStopEmissionFlag = false;
                                            }
                                            AbilityNow = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                            AbilityTriggerTime = 0;
                                    }
                                }
                                else if (hit.collider.gameObject.tag == "Floor&Stop")
                                {
                                    AbilityTriggerTime += Time.deltaTime;
                                    if (3 <= AbilityTriggerTime)
                                    {
                                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                        {
                                            hit.collider.gameObject.SendMessage("CollStop");
                                            Instantiate(CFX2_PickupDiamond2, hit.point + (hit.normal * 3), Quaternion.identity);
                                            audioSource.PlayOneShot(stopSE);
                                            if (StopObjectName == null)
                                            {
                                                StopObjectName = hit.collider.gameObject.transform.root.name;
                                                if (hit.collider.gameObject.transform.root.GetComponent<MeshRenderer>())
                                                    hit.collider.gameObject.transform.root.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                                                AbilityStopEmissionFlag = true;
                                            }
                                            else
                                            {
                                                StopObjectName = null;
                                                if (hit.collider.gameObject.transform.root.GetComponent<MeshRenderer>())
                                                    hit.collider.gameObject.transform.root.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                                                AbilityStopEmissionFlag = false;
                                            }
                                            AbilityNow = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                            AbilityTriggerTime = 0;
                                    }
                                }
                                else
                                {
                                    AbilityTriggerTime = 0;
                                }
                            }
                            else
                            {
                                AbilityTriggerTime = 0;
                            }
                        }
                        break;
                    case 3: //break
                        {
                            if (hit.collider.gameObject.tag == "Break&Wall")
                            {
                                AbilityTriggerTime += Time.deltaTime;
                                if (3 <= AbilityTriggerTime)
                                {
                                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                    {
                                        hit.collider.gameObject.SendMessage("CollBreak");
                                        audioSource.PlayOneShot(hakaiSE);
                                        AbilityNow = 0;
                                        //AbilityPenalty(BreakAbilityPenaltyTime);
                                        NextUse();
                                        Instantiate(CFX_Explosion_B_Smoke, hit.point + (hit.normal * 3), Quaternion.identity);
                                    }
                                }
                                else
                                {
                                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                        AbilityTriggerTime = 0;
                                }
                            }
                            else
                                 if (hit.collider.gameObject.tag == "BreakItem")
                            {
                                AbilityTriggerTime += Time.deltaTime;
                                if (3 <= AbilityTriggerTime)
                                {
                                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                    {
                                        if (hit.collider.gameObject.GetComponent<MeshRenderer>())
                                        {
                                            hit.collider.gameObject.GetComponent<MeshCollider>().enabled = false;
                                            hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
                                        }
                                        else
                                        {
                                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                                            hit.collider.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                                            hit.collider.gameObject.GetComponent<MeshCollider>().enabled = false;
                                            hit.collider.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                                        }
                                        AbilityNow = 0;
                                        //AbilityPenalty(BreakAbilityPenaltyTime);
                                        NextUse();
                                        Instantiate(CFX_Explosion_B_Smoke, hit.point + (hit.normal * 3), Quaternion.identity);
                                    }
                                }
                                else
                                {
                                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                        AbilityTriggerTime = 0;
                                }
                            }
                            else
                            {
                                AbilityTriggerTime = 0;
                            }
                        }
                        break;
                }
            }
            else
            {
                carsor.handflag = false;
                AbilityTriggerTime = 0;
                reticleNoAction();
                if (WarpParticleFlag)
                {
                    Destroy(Instance);
                    Destroy(InstanceGeat);
                }
                WarpParticleFlag = false;
            }
            //Debug.DrawRay(ray.origin, ray.direction * raylong, Color.blue, 5);
        }
    }
    //レティクルがオブジェクトの上に表示される用。
    void reticleAction(Vector3 normal, Vector3 point)
    {
        switch (AbilityNow)
        {
            case 0:
                break;
            case 1:
                reticleWarp.transform.rotation = Quaternion.LookRotation(normal + Camera.main.transform.up * 0.01f);
                reticleWarp.transform.position = point + (normal * 5);
                CircleWarp.transform.rotation = Quaternion.LookRotation(normal + Camera.main.transform.up * 0.01f);
                CircleWarp.transform.position = point + (normal * 5);
                break;
            case 2:
                reticleStop.transform.rotation = Quaternion.LookRotation(normal + Camera.main.transform.up * 0.01f);
                reticleStop.transform.position = point + (normal * 5);
                CircleStop.transform.rotation = Quaternion.LookRotation(normal + Camera.main.transform.up * 0.01f);
                CircleStop.transform.position = point + (normal * 5);
                break;
            case 3:
                reticleBreak.transform.rotation = Quaternion.LookRotation(normal + Camera.main.transform.up * 0.01f);
                reticleBreak.transform.position = point + (normal * 5);
                CircleBreak.transform.rotation = Quaternion.LookRotation(normal + Camera.main.transform.up * 0.01f);
                CircleBreak.transform.position = point + (normal * 5);
                break;
        }
    }
    //Rayがオブジェクトに当たってない時
    void reticleNoAction()
    {
        switch (AbilityNow)
        {
            case 0:
                break;
            case 1:
                reticleWarp.transform.rotation = Camera.main.transform.rotation;
                reticleWarp.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 150);
                break;
            case 2:
                reticleStop.transform.rotation = Camera.main.transform.rotation;
                reticleStop.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 150);
                break;
            case 3:
                reticleBreak.transform.rotation = Camera.main.transform.rotation;
                reticleBreak.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 150);
                break;
        }
    }
    //能力発動用チャージ用
    void Charge(float ATime)
    {
        coolcircle.fillAmount = 1.0f / BreakEyeCoolTime * (NextUseTime);
        switch (AbilityNow) {
            case 0: break;
            case 1:
                CircleWarp.GetComponent<Image>().fillAmount = 1.0f / 3.0f * ATime;
                break;
            case 2:
                CircleStop.GetComponent<Image>().fillAmount = 1.0f / 3.0f * ATime;
                break;
            case 3:
                CircleBreak.GetComponent<Image>().fillAmount = 1.0f / 3.0f * ATime;
                break;
            }
    }

    //能力発動時のペナルティー用
    void AbilityPenalty(int time)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage1":
                Stage1TimeManager.PenaltyTime(time);
                break;
            case "Stage2":
                Stage2TimeManager.PenaltyTime(time);
                break;
            case "Stage3":
                Stage3TimeManager.PenaltyTime(time);
                break;
        }
    }

    //モノクル入手
    void GetMonocle()
    {
        EyeLongFlag = true;
    }
    //破壊能力クールタイム
    void NextUse()
    {
        NextUseTime = BreakEyeCoolTime;
    }
}

