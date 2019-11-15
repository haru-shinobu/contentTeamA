using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    Vector3 FirstPos;
    public int FallStartTime;
    float CountTimer;
    bool Flag = true;
    bool GravityFlag;
    Rigidbody rb;
    
    void Start()
    {
        tag = "Floor&Stop";
        FirstPos = transform.position;
        CountTimer = 0;
        rb = gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.AddComponent<BoxCollider>();
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<Rigidbody>().freezeRotation = true;
        
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | 
            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        GravityFlag = false;
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (Flag)
        {
            if (col.gameObject.tag == "Player")
                CountTimer = FallStartTime;
        }
        if (col.gameObject.tag != "Player")
            //GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject);
    }

    void Update()
    {
        if (0 < CountTimer)
        {
            CountTimer -= Time.deltaTime;
            if (CountTimer <= 0)
            {
                GravityFlag = true;
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        if (GravityFlag)
            rb.AddForce(-transform.up * 100, ForceMode.Force);
    }
    void CollStop()
    {
        Flag = !Flag;
    }
}
