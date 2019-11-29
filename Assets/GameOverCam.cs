using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCam : MonoBehaviour
{
    Vector3 pos;
    bool Flag;
    int count;
    Quaternion rot;
    GameObject player;
    int Cou;
    void Start()
    {
        Flag = false;
        count = 0;
        Cou = 0;
        player = GameObject.Find("Player");
        pos = player.transform.position - Vector3.up * player.transform.localScale.y * 0.5f;
        rot = player.transform.rotation;
        Instantiate(Resources.Load<Canvas>("GameOverShade"));
        //Instantiate(Resources.Load<Canvas>("GameOverDissolve"));
    }

    void Update()
    {
        float Rot = transform.localRotation.eulerAngles.z;

        if (Cou < 2)
        {
            Vector3 Forward = player.transform.forward;
            Quaternion IRot = transform.rotation;
            transform.rotation = rot;
            
            if (!Flag)
            {
                if (count++ < 20)
                    transform.RotateAround(pos, Forward, Time.deltaTime * 50);
                else
                {
                    Flag = !Flag;
                    Cou++;
                }
            }
            else
            {
                if (-20 < count--)
                    transform.RotateAround(pos, Forward, -Time.deltaTime * 50);
                else
                    Flag = !Flag;
            }
            transform.rotation = IRot;
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0,0.7f,0.7f,0.1f), Time.deltaTime * 2);
        }
    }
}
