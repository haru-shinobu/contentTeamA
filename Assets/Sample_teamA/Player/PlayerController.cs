﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector3 defaultScale = Vector3.zero;
    private float JumpVertical;
    public float moveSpeed;
    public float JumpForce; // 145で身長と同じくらい。Scaley=150。
    float Speed;
    float JumpTime;
    private float PlayerScale;
    private bool JumpEnd = false;
    public bool Ground = false;
    public bool PlayerAbility;//RayAbilityから書き換える
    bool Sky = false;
    float skyY;
    public bool UseLongJump = false;

    float inputVertical;
    Rigidbody rb;

    //リセット能力用
    static Vector3 ResetPos;
    public bool ResetFlag;
    GameStageSetting Setting;
    void Start()
    {
        ResetFlag = false;
        Setting = GameObject.Find("GameMaster").GetComponent<GameStageSetting>();
        tag = "Player";
        gameObject.AddComponent<Rigidbody>();
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().freezeRotation = true;
        gameObject.AddComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        PlayerScale = gameObject.transform.localScale.x * 0.4f;//0.26で8秒 0.4で6秒ほど？
        //PlayerScale　20と13になる…
        Speed = moveSpeed;
        PlayerAbility = false;
        if (Setting.ResetStatus)
        {
            gameObject.transform.position = ResetPos;
        }
        else
        {
            ResetPos = transform.position;
        }
        defaultScale = transform.lossyScale;
    }

    void OnTriggerEnter(Collider col)
    {
        JumpVertical = 0;
        if (col.gameObject.tag == "Floor")
        {
            Ground = true;
            Sky = false;
        }
        if (col.gameObject.tag == "Floor&Stop")
        {
            Ground = true;
            Sky = false;
        }
        if (col.gameObject.tag == "Move&Stop")
        {
            Ground = true;
            Sky = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Floor")
        {
            Sky = true;
            skyY = transform.position.y;
        }
        if (col.gameObject.tag == "Floor&Stop")
        {
            Sky = true;
            skyY = transform.position.y;
        }
        if (col.gameObject.tag == "Move&Stop")
        {
            Sky = true;
            skyY = transform.position.y;
        }
    }

    void Update()
    {
        if (Sky && Ground)
            if (skyY - 1 > transform.position.y)
                Ground = Sky = false;

        if (GetComponent<Rigidbody>().isKinematic && Input.anyKeyDown)
            GetComponent<Rigidbody>().isKinematic = false;

        if (!PlayerAbility)//能力使用時の入力制限
        {
            //EditのProjectSettings...でInput項目Verticalをupとｗ以外消去しておく
            inputVertical = Input.GetAxisRaw("Vertical");
            JumpProcess();
        }
        else
        {
            if (ResetFlag)
            {
                ReStartPosMem();
            }
        }
        FallProcess();
        
        Vector3 lossScale = transform.lossyScale;
        Vector3 localScale = transform.localScale;

        transform.localScale = new Vector3(
                localScale.x / lossScale.x * defaultScale.x,
                localScale.y / lossScale.y * defaultScale.y,
                localScale.z / lossScale.z * defaultScale.z);


        //画面外落下でシーンリロード.ResetPosから再開
        if (transform.position.y < -50)
            Setting.ReStartScene();
    }

    void FixedUpdate()
    {
        float vert = 0;
        if (Ground)
        {
            vert = inputVertical * PlayerScale;
        }
        else
        {
            vert = JumpVertical * PlayerScale;
        }
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward;
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * vert;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * Speed + new Vector3(0, rb.velocity.y, 0);
        
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    private void JumpProcess()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Ground)
        {
            JumpVertical = inputVertical;
            Ground = false;
            JumpTime = 1;
            rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
            if (UseLongJump)
            {
                GetComponent<Rigidbody>().useGravity = false;
                JumpEnd = false;
            }
            else
                JumpEnd = true;
        }
        if (Sky && Ground)
            JumpVertical = inputVertical;
    }
    private void FallProcess()
    {
        if (UseLongJump)
        {
            if (!Ground && !JumpEnd)
            {
                JumpTime -= Time.deltaTime;
                if (JumpTime <= 0)
                {
                    JumpEnd = true;
                    JumpTime = 1;
                }
            }
            else
            {
                if (!JumpEnd)
                    JumpEnd = true;
            }
            if (Input.GetKeyUp(KeyCode.Space) || JumpEnd)
            {
                JumpEnd = true;
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
        if (!Ground && JumpEnd)
        {
            rb.AddForce(-transform.up * JumpForce, ForceMode.Force);
        }
        else
        {
            if (JumpEnd)
                if (5 < rb.velocity.y)
                    rb.AddForce(-transform.up * JumpForce * 0.2f, ForceMode.Impulse);
        }
    }
    private void ReStartPosMem()
    {
        ResetPos = transform.position + new Vector3(0, 5, 0);
        Setting.ResetStatus = true;
        Setting.ReStartScene();
    }
}

