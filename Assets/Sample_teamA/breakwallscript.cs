using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakwallscript : MonoBehaviour
{
    bool Flag = false;
    public float BreakLateTimer = 0;
    BoxCollider secand;
    //GameObject BreakFrame;
    void Start()
    {
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        secand = transform.gameObject.AddComponent<BoxCollider>();
        secand.isTrigger = true;
    }
    void Update()
    {
        if (Flag)
        {
            BreakLateTimer -= Time.deltaTime;
            if (BreakLateTimer < 0) 
            {
                transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                secand.enabled = false;
                transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                
            }
        }
    }
    void CollBreak()
    {
        Flag = true;
        BreakLateTimer = 0.2f;
    }
}
