using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayAbility : MonoBehaviour
{

    public GameObject Player;
    public GameObject reticleWarp;  //カーソル用
    public GameObject reticleStop;  //カーソル用
    public GameObject reticleBreak; //カーソル用
    public GameObject reticleHand;  //カーソル用
    public GameObject clock;
    public int BreakAbilityPenaltyTime;//ペナルティ用
    public int ResetAbilityPenaltyTime;//ペナルティ用

    public GameObject exploision;
    public GameObject WarpLoad;
    public GameObject WarpGeat;


    public float AbilityChengeMenuTime; //メニュー表示用。あとで画像とかで表示するようにしないと…
    public int AbilityNum;              //能力いくつもってるか
    float KeyQTime = 0;                 //能力メニュー開くための長押し判定用
    Vector3 center;                     //視線をとるため
    bool AbilityFlag;                   //能力行使中か否か
    float AbilityTriggerTime = 0;       //各種能力発動までのカウント用
    protected string StopObjectName;    //Stop能力で別オブジェクトが反応しないようにするため
    public int AbilityNow;              //現在発動中の能力0～5
    float NextUseTime = 0;//クールタイム用
    bool WarpParticleFlag;
    GameObject Instance;
    GameObject InstanceGeat;
    void Start()
    {
        center = new Vector3(Screen.width / 2, Screen.height / 2);
        AbilityFlag = false;
        Player = GameObject.Find("Player");
        AbilityNow = 0;
        StopObjectName = null;
        reticleWarp = GameObject.Find("WarpImage");
        reticleStop = GameObject.Find("StopImage");
        reticleBreak = GameObject.Find("BreakImage");
        reticleHand = GameObject.Find("HandImage");
        clock = GameObject.Find("clock");
        WarpParticleFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        AbilitycodeQ();
        AbilityAction();
    }


    void AbilitycodeQ()
    {
        //クールタイム用
        if (0 < NextUseTime)
            NextUseTime -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Q))
            KeyQTime = 0;
        if (Input.GetKey(KeyCode.Q))
            KeyQTime += Time.deltaTime;

        if (AbilityChengeMenuTime < KeyQTime)//MenuOpenしてるとき
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


        if (Input.GetKeyUp(KeyCode.Q))
            AbilityTriggerTime = 0;


        if (AbilityNow != 0)
        {
            AbilityFlag = true;
            Player.GetComponent<PlayerController>().PlayerAbility = true;
        }
        else
        {
            AbilityFlag = false;
            Player.GetComponent<PlayerController>().PlayerAbility = false;
        }
    }
    //Rayを飛ばしてる部分
    void AbilityAction()
    {
        if (AbilityFlag)
        {
            Ray ray = Camera.main.ScreenPointToRay(center);
            RaycastHit hit;
            float raylong;
            if (5 == AbilityNow)
            {
                raylong = Player.transform.localScale.x * 2;
            }
            else
            {
                raylong = 1000f;
            }

            if (Physics.Raycast(ray, out hit, raylong))
            {
                //位置座標を取得してみている
                //Debug.Log(hit.collider.gameObject.transform.position);
                //Debug.Log((int)AbilityTriggerTime);

                reticleAction(hit.normal, hit.point);
                if (AbilityNow != 1 && WarpParticleFlag)
                {
                    Destroy(Instance);
                    Destroy(InstanceGeat);
                }
                switch (AbilityNow)
                {
                    case 0: break;
                    case 1: //warp

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
                                if (Input.GetKeyDown(KeyCode.E))
                                {
                                    Player.gameObject.transform.position = hit.collider.gameObject.transform.position;
                                    AbilityNow = 0;
                                }
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
                        break;
                    case 2: //stop
                        if (StopObjectName == hit.collider.gameObject.transform.root.name || StopObjectName == null)
                        {
                            if (hit.collider.gameObject.tag == "move&Stop")
                            {
                                AbilityTriggerTime += Time.deltaTime;
                                if (3 <= AbilityTriggerTime)
                                {
                                    if (Input.GetKeyDown(KeyCode.E))
                                    {
                                        hit.collider.gameObject.SendMessage("CollStop");
                                        if (StopObjectName == null)
                                        {
                                            StopObjectName = hit.collider.gameObject.transform.root.name;
                                            hit.collider.gameObject.transform.root.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                                        }
                                        else
                                        {
                                            StopObjectName = null;
                                            hit.collider.gameObject.transform.root.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                                        }
                                        AbilityNow = 0;
                                    }
                                }
                            }
                            else
                        if (hit.collider.gameObject.tag == "Floor&Stop")
                            {
                                AbilityTriggerTime += Time.deltaTime;
                                if (3 <= AbilityTriggerTime)
                                {
                                    if (Input.GetKeyDown(KeyCode.E))
                                    {
                                        hit.collider.gameObject.SendMessage("CollStop");
                                        if (StopObjectName == null)
                                        {
                                            StopObjectName = hit.collider.gameObject.transform.root.name;
                                            hit.collider.gameObject.transform.root.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                                        }
                                        else
                                        {
                                            StopObjectName = null;
                                            hit.collider.gameObject.transform.root.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                                        }
                                        AbilityNow = 0;
                                    }
                                }
                            }
                            else
                            {
                                AbilityTriggerTime = 0;
                            }
                        }
                        break;
                    case 3: //break
                        if (hit.collider.gameObject.tag == "Break&Wall")
                        {
                            AbilityTriggerTime += Time.deltaTime;
                            if (3 <= AbilityTriggerTime)
                            {
                                if (Input.GetKeyDown(KeyCode.E))
                                {
                                    hit.collider.gameObject.SendMessage("CollBreak");
                                    AbilityNow = 0;
                                    AbilityPenalty(BreakAbilityPenaltyTime);
                                    NextUseTime = 10;
                                    Instantiate(exploision, hit.point + (hit.normal * 3), Quaternion.identity);
                                }
                            }
                        }
                        break;
                    case 4: //reset
                        AbilityTriggerTime += Time.deltaTime;
                        if (3 <= AbilityTriggerTime)
                        {
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                AbilityPenalty(ResetAbilityPenaltyTime);
                                Player.GetComponent<PlayerController>().ResetFlag = true;
                            }
                        }
                        break;
                    case 5:
                        if (hit.collider.gameObject.tag == "Switch")
                        {
                            AbilityTriggerTime += Time.deltaTime;
                            if (3 <= AbilityTriggerTime)
                            {
                                hit.collider.gameObject.SendMessage("TriggerOn");
                                AbilityNow = 0;
                            }
                        }
                        break;
                }
            }
            else
            {
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
            case 0: break;
            case 1:
                reticleWarp.transform.rotation = Quaternion.LookRotation(normal);
                reticleWarp.transform.position = point + (normal * 5);
                break;
            case 2:
                reticleStop.transform.rotation = Quaternion.LookRotation(normal);
                reticleStop.transform.position = point + (normal * 5);
                break;
            case 3:
                reticleBreak.transform.rotation = Quaternion.LookRotation(normal);
                reticleBreak.transform.position = point + (normal * 5);
                break;
            case 4: break;
            case 5:
                reticleHand.transform.rotation = Quaternion.LookRotation(normal);
                reticleHand.transform.position = point + (normal * 5);
                break;
        }
    }
    //Rayがオブジェクトに当たってない時
    void reticleNoAction()
    {
        switch (AbilityNow)
        {
            case 0: break;
            case 1:
                reticleWarp.transform.rotation = Camera.main.transform.rotation;
                reticleWarp.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 50);
                break;
            case 2:
                reticleStop.transform.rotation = Camera.main.transform.rotation;
                reticleStop.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 50);
                break;
            case 3:
                reticleBreak.transform.rotation = Camera.main.transform.rotation;
                reticleBreak.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 50);
                break;
            case 4: break;
            case 5:
                reticleHand.transform.rotation = Camera.main.transform.rotation;
                reticleHand.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 100);
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
}

