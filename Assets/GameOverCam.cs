using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCam : MonoBehaviour
{
    Vector3 pos;
    bool Flag;
    int count;
    Quaternion rot;
    void Start()
    {
        Flag = false;
        count = 0;
        pos = GameObject.Find("Player").transform.position - Vector3.up * transform.localScale.y * 0.5f;
        rot = transform.rotation;
    }

    void Update()
    {
        float Rot = transform.localRotation.eulerAngles.z;
        Debug.Log(Rot);
        count =50;
        //if (count > 100) count = 0;

        /*
        if (!Flag)
        {
            if (Rot < 20)
                transform.RotateAround(pos, transform.forward, Time.deltaTime * count);
            else
                Flag = true;
        }
        else
        {
            if (0 < Rot)
                transform.RotateAround(pos, transform.forward, -Time.deltaTime * count);
            else
                Flag = false;
        }
          */  

    }
}
