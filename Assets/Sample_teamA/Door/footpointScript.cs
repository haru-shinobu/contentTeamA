using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footpointScript : MonoBehaviour
{
    RectTransform RightFoot;
    RectTransform LeftFoot;
    GameObject Player;
    Vector3 Distance;
    Vector2 pos,Lpos, posfront, posback;
    PlayerController PController;
    bool footFlag;
    float Timer;
    void Start()
    {
        RightFoot = gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        LeftFoot = gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        Player = GameObject.Find("Player");
        PController = Player.GetComponent<PlayerController>();
        Distance = -Player.transform.up * Player.transform.localScale.y * 0.49f;
        pos = RightFoot.localPosition;
        posfront = pos + new Vector2(0, 20)*2;
        posback = pos + new Vector2(0, -20)*2;
        Lpos = new Vector2(24, 0);
        footFlag = false;
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + Distance;
        transform.localRotation = Player.transform.localRotation * new Quaternion(1,0,0,1);
        if (PController.PlayerWalk)
        {
            Timer += Time.deltaTime;
            if (1 <= Timer)
            {
                footFlag = !footFlag;
                Timer -= 1;
            }
            if (footFlag)
            {
                RightFoot.localPosition = Vector3.Lerp(RightFoot.localPosition, posfront, Time.deltaTime);
                LeftFoot.localPosition = Vector3.Lerp(LeftFoot.localPosition, Lpos + posback, Time.deltaTime*1.1f);
            }
            else
            {
                RightFoot.localPosition = Vector3.Lerp(RightFoot.localPosition, posback, Time.deltaTime*1.1f);
                LeftFoot.localPosition = Vector3.Lerp(LeftFoot.localPosition, Lpos + posfront, Time.deltaTime);
            }
        }
        else
        {
            RightFoot.localPosition = pos;
            LeftFoot.localPosition = pos + Lpos;
        }
    }
}