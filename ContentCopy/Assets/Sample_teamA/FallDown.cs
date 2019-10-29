using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    Vector3 FirstPos;
    public int FallStartTime;
    int Time;
    bool Flag = true;
    
    void Start()
    {
        tag = "Floor&Stop";
        FirstPos = transform.position;
        Time = 0;
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.AddComponent<BoxCollider>();
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().freezeRotation = true;

    }
    
    void OnTriggerEnter(Collider col)
    {
        if (Flag)
        {
            if (col.gameObject.tag == "Player")
                Time = FallStartTime;
            if (col.gameObject.tag != "Player")
                GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Update()
    {
        if (0 < Time)
            if (--Time <= 0)
                GetComponent<Rigidbody>().isKinematic = false;
    }
    void CollStop()
    {
        Flag = !Flag;
    }
    /*
    void CollReSet()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = FirstPos;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
    */
}
