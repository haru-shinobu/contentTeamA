using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doormove : MonoBehaviour
{
    
        AudioSource audioSource;
    public AudioClip doaSE;

    public GameObject ThruWall;
    public GameObject ThruWallReverse;
    bool OpenFlag;
    bool CloseFlag;
    bool thruFlag;
    float rot;
    int Timer;
    Vector3 posL,posR;
    Vector3 SRPos, SLPos;
    Quaternion SRRot, SLRot;
    Quaternion Rota;
    GameObject DoorCollide;
    GameObject DoorRight;
    GameObject DoorLeft;
    public Texture DoorImage;

    bool PerceptionFlag;
    public int PerceptionTime;
    //ドア通過判定後のプレイヤー保存
    GameObject Player;

    GameTimerDirector TimeDirec;
    void Start()
    {
        doaSE = (AudioClip)Resources.Load("doa-kaihei");
        audioSource = GetComponent<AudioSource>();

        Player = GameObject.Find("Player");
        float roty = gameObject.transform.rotation.y;
        float rotw = gameObject.transform.rotation.w;
        Rota = gameObject.transform.rotation;

        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        Timer = 0;
        OpenFlag = false;
        CloseFlag = false;
        PerceptionFlag = false;
        DoorRight = transform.GetChild(0).gameObject;
        DoorLeft = transform.GetChild(1).gameObject;
        
        DoorCollide = Instantiate(DoorRight.transform.gameObject,transform.position + new Vector3(0,DoorRight.transform.localScale.y*0.5f,0),new Quaternion(0,0,0,0),transform);
        DoorCollide.transform.name = "Door";
        //DoorCollide.gameObject.GetComponent<MeshRenderer>().enabled = false;
        DoorCollide.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", DoorImage);
        DoorCollide.gameObject.transform.GetComponent<BoxCollider>().isTrigger = true;
        DoorCollide.transform.localScale = new Vector3(DoorRight.transform.localScale.x * 1.9f, DoorRight.transform.localScale.y, DoorRight.transform.localScale.z*0.5f);
        Destroy(DoorCollide.transform.GetComponent<Doormove>());
        DoorCollide.transform.gameObject.AddComponent<DoorPassDiscrimination>().Target = gameObject;
        gameObject.transform.rotation = Rota;

        SRPos = DoorRight.transform.position;
        SLPos = DoorLeft.transform.position;
        SRRot = DoorRight.transform.localRotation;
        SLRot = DoorLeft.transform.localRotation;
        posR = transform.position + transform.right * DoorRight.transform.localScale.x;
        posL = transform.position - transform.right * DoorRight.transform.localScale.x;
        Vector3 rotangle = Rota.eulerAngles;

        TimeDirec = GameObject.Find("GameMaster").GetComponent<GameTimerDirector>();
    }

    
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        int speed = 50;

        Quaternion rta = DoorRight.transform.localRotation;
        Vector3 RoAngle = rta.eulerAngles;
        Quaternion lta = DoorLeft.transform.localRotation;
        Vector3 LoAngle = lta.eulerAngles;

        if (OpenFlag)
        {
            
            if (RoAngle.y < 90)
                DoorRight.transform.RotateAround(posR, transform.up, Time.deltaTime * speed);
            if (270 < LoAngle.y || LoAngle.y < 1)
                DoorLeft.transform.RotateAround(posL, transform.up, -Time.deltaTime * speed);
            else
            {
                OpenFlag = !OpenFlag;
                thruFlag = true;
                PerceptionFlag = false;
            }
        }
        if (CloseFlag)
        {
            if (1 < RoAngle.y)
                DoorRight.transform.RotateAround(posR, transform.up, -Time.deltaTime * speed);
            if (1 < LoAngle.y && 268 < LoAngle.y)
                DoorLeft.transform.RotateAround(posL, transform.up, Time.deltaTime * speed);
            else
            {
                CloseFlag = !CloseFlag;
                thruFlag = false;

                DoorRight.transform.position = SRPos;
                DoorLeft.transform.position = SLPos;
                DoorRight.transform.localRotation = SRRot;
                DoorLeft.transform.localRotation = SLRot;
                
                ThruWall.transform.GetChild(0).transform.GetComponent<MeshCollider>().enabled = true;
                ThruWallReverse.transform.GetChild(0).transform.GetComponent<MeshCollider>().enabled = true;
            }
        }

        if (0 < Timer)
        {
            Timer--;
            if (Timer <= 0)
            {
                Timer = 0;
                CloseSwitch();
                TimeDirec.DoorPass();//時間表示用フラグ
            }
        }
        if (PerceptionFlag)
        {
            Timer = PerceptionTime;
        }
    }
    public void switching()
    {
        audioSource.PlayOneShot(doaSE);
        OpenFlag = true;
    }
    public void TriggerOn()
    {       //  TriggerOn
        audioSource.PlayOneShot(doaSE);
        OpenFlag = true;
    }
    //スイッチ式からの入力
    void SwichChange()
    {//  SwichChange
        audioSource.PlayOneShot(doaSE);
        OpenFlag = true;
        
    }
    //感圧式からの入力
    void PerceptionChange()
    {
        if (!CloseFlag)
        {
            OpenFlag = true;
            audioSource.PlayOneShot(doaSE);
            PerceptionFlag = true;
        }
    }


    public void CloseSwitch()
    {
        
        CloseFlag = !CloseFlag;
        audioSource.PlayOneShot(doaSE);
    }
    public void Pass()
    {
        Timer = 90;
    }
    public void ThruPass(bool bFlag)
    {
     if(thruFlag)
            if (bFlag)
            {
                ThruWall.transform.GetChild(0).transform.GetComponent<MeshCollider>().enabled = false;
                ThruWallReverse.transform.GetChild(0).transform.GetComponent<MeshCollider>().enabled = false;
            }
            else
            {
                ThruWall.transform.GetChild(0).transform.GetComponent<MeshCollider>().enabled = true;
                ThruWallReverse.transform.GetChild(0).transform.GetComponent<MeshCollider>().enabled = true;
                Player.GetComponent<PlayerController>().SavePoint(transform.position,-transform.forward);
            }
    }
}