using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PendulumShaft : MonoBehaviour
{
    Vector3 rot;
    int time;
    public float Angle;
    bool Flag = false;
    public bool StopFlag;
    void Start()
    {
        rot = transform.rotation.eulerAngles;
        time = 0;
        StopFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!StopFlag)
        {
            time++;
            if (180 <= time)
            {
                Flag = !Flag;
                time = 0;
            }

            var sin = 20 * Math.Sin((2 * Mathf.PI / 180 * (time)));
            Angle = (float)sin;

            transform.eulerAngles = new Vector3(rot.x + Angle, rot.y, rot.z);
        }
    }
}
