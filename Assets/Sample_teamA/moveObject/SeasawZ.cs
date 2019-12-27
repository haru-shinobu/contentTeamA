using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasawZ : MonoBehaviour
{
    Rigidbody rigid;
    bool Flag = false;

    void Start()
    {
        tag = "Floor&Stop";
        if (!gameObject.GetComponent<Rigidbody>())
            gameObject.AddComponent<Rigidbody>().useGravity = false;
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.angularDrag = 40.0f;
        rigid.constraints = RigidbodyConstraints.FreezePosition |
        RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;        
    }
    
    //角度制限をつけたいシーソー。
    

    void CollReSet()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
    void CollStop()
    {
        Flag = !Flag;
        if (Flag)
            rigid.constraints = RigidbodyConstraints.FreezeAll;
        else
            rigid.constraints = RigidbodyConstraints.FreezePosition |
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

    }
}
