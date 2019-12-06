using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraController : MonoBehaviour
{
    GameObject Player;
    Vector3 targetPos, CameraPos, CameraEyePos;
    Camera cam;
    bool SceneEndFlag;
    bool MouseControl;
    public bool CamControllFlag;
    Vector3 goalpos;
    void Start()
    {
        Player = GameObject.Find("Player");
        tag = "MainCamera";
        CameraEyePos = new Vector3(0, Player.transform.localScale.y * 0.4f, 0);
        targetPos = Player.transform.position;
        transform.position = Player.transform.position + CameraEyePos;
        cam = GetComponent<Camera>();
        MouseControl = GameObject.Find("GameMaster").GetComponent<GameStageSetting>().MouseMode;
        CamControllFlag = true;
        //goalpos = GameObject.Find("GoalFootSwitch").transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Player.transform.position - targetPos;
        targetPos = Player.transform.position;
        CameraPos = this.transform.position;

        float InputX = 0;
        float InputY = 0;
        if (CamControllFlag)
        {
            if (!MouseControl)
            {
                // キーを押している間
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        InputX = -1;
                    }
                    else
                    if (Input.GetKey(KeyCode.D))
                    {
                        InputX = 1;
                    }
                    else
                    if (Input.GetKey(KeyCode.W))
                    {
                        InputY = 1;
                    }
                    else
                        if (Input.GetKey(KeyCode.S))
                    {
                        InputY = -1;
                    }
                }
            }
            else
            {
                InputX = Input.GetAxisRaw("Mouse X");
                InputY = Input.GetAxisRaw("Mouse Y");
            }
        }
        else
        {
            goalpos = GameObject.Find("GoalwatchPointer").transform.position;
            gameObject.transform.LookAt(goalpos, Vector3.right);
            gameObject.transform.LookAt(goalpos, Vector3.up);   
        }
        // targetの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(CameraPos, Vector3.up, InputX * Time.deltaTime * 200f);
        transform.RotateAround(CameraPos, transform.right, InputY * Time.deltaTime * 200f);
        
    }
}