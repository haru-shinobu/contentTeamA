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
    public bool ElevatorFlag;
    Vector3 vel;
    void Start()
    {
        tag = "Floor&Stop";
        FirstPos = transform.position;
        CountTimer = 0;
        rb = gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.AddComponent<BoxCollider>();
        rb.useGravity = true;
        rb.isKinematic = true;
        vel = new Vector3(0, 0, 0);
        //GetComponent<Rigidbody>().freezeRotation = true;
        
        rb.constraints = RigidbodyConstraints.FreezePositionX | 
            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        GravityFlag = false;
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (Flag)
        {
            if (col.gameObject.tag == "Player")
            {
                CountTimer = FallStartTime;
                Flag = false;
            }
        }
        if (col.gameObject.tag != "Player")
        {
            if(ElevatorFlag)
            GameObject.Find("Elevator").GetComponent<MoveUpDownFloor>().MoveFlag = true;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (Flag)
        {
            if (0 < CountTimer)
            {
                CountTimer -= Time.deltaTime;
                if (CountTimer <= 0)
                {
                    GravityFlag = true;
                    rb.isKinematic = false;
                }
            }
            if (GravityFlag)
            {
                rb.AddForce(-transform.up * 100, ForceMode.Force);
            }
        }
        else
        {
            if (GravityFlag)
            {
                rb.velocity *= 0;
            }
        }
    }
    void CollStop()
    {
        Flag = !Flag;
        if (GravityFlag)
            vel = rb.velocity;
        else
            rb.velocity = vel;
    }
}
