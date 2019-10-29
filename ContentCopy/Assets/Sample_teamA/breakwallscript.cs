using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakwallscript : MonoBehaviour
{
    bool Flag = false;
    int Time = 0;
    GameObject BreakFrame;
    void Start()
    {
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        transform.gameObject.AddComponent<BoxCollider>().isTrigger = true;
        BreakFrame = GameObject.Find("fure-mu");
        Instantiate(BreakFrame,transform.position - new Vector3(0,transform.localScale.y*0.5f),new Quaternion(0,0,0,0),transform);
        transform.GetChild(0).gameObject.transform.localScale *= 0.001f;
        transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void CollBreak()
    {
        //pa-thicleSystem coll
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
