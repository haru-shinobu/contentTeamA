using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyorscript : MonoBehaviour
{
    bool Flag=false;
    bool moveFlag = true;
    GameObject Player;
    void Start()
    {
        tag = "Floor&Stop";
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player = col.gameObject;
            Flag = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
            Flag = false;
    }

    void Update()
    {
        if (moveFlag)
            if (Flag)
                Player.transform.position -= transform.forward*2;
        
    }

    void CollStop()
    {
        moveFlag = !moveFlag;
    }
}
