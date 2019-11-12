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
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().angularDrag = 30.0f;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition |
        RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        rigid = gameObject.GetComponent<Rigidbody>();
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
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        else
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition |
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

    }
}
