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
        //BreakFrame = GameObject.Find("fure-mu");
        //Instantiate(BreakFrame,transform.position - new Vector3(0,transform.localScale.y*0.5f),new Quaternion(0,0,0,0),transform);
        //transform.GetChild(0).gameObject.transform.localScale *= 0.001f;
        //transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
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
                //transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
    void CollBreak()
    {
        Flag = true;
        BreakLateTimer = 0.2f;
    }
}
