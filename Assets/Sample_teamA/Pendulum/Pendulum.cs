using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Rigidbody rigid;
    GameObject WayTrigger;
    public float PendulumHigh;
    public float PendulumLong;
    void Start()
    {
        tag = "Floor&Stop";
        WayTrigger = gameObject;
        transform.gameObject.GetComponent<BoxCollider>().enabled = false;
        ShaftCreate();
        PoleRightCreate();
        PoleLeftCreate();
        
//pole Shaft        
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(1.1f, 0.8f, 0.8f);
        transform.GetChild(0).gameObject.AddComponent<Rigidbody>().isKinematic = true;
        transform.GetChild(0).gameObject.AddComponent<PendulumShaft>();
        rigid = gameObject.GetComponent<Rigidbody>();
 
        BodyShaftCreate();
        
    }

    void ShaftCreate()//pole Shaft
    {
        //名、POS、ROTATE、Parent
        Instantiate(WayTrigger, transform.position, new Quaternion(0, 0, 0, 0), transform);
        Destroy(transform.GetChild(0).gameObject.transform.GetComponent<Pendulum>());
        transform.GetChild(0).gameObject.name = "PendulumPoleShaft";
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(1.1f,1,1);
        transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    void PoleRightCreate()        //right Pole
    {
        Vector3 Create = transform.position + -transform.up * PendulumHigh * 0.5f + transform.right * transform.localScale.x*0.5f;
        Instantiate(transform.GetChild(0).gameObject, Create, new Quaternion(0, 0, 0, 0), transform.GetChild(0).gameObject.transform);
        GameObject target = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        target.name = "PendulumPoleRight";
        target.transform.localScale = (new Vector3(0.05f, PendulumHigh*0.2f, 0.5f));
        target.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        target.tag = "wall";

    }
    void PoleLeftCreate()        //left Pole
    {
        Vector3 Create = transform.position + -transform.up * PendulumHigh * 0.5f - transform.right * transform.localScale.x *0.5f;
        Instantiate(transform.GetChild(0).gameObject.transform.GetChild(0).gameObject, Create, new Quaternion(0, 0, 0, 0), transform.GetChild(0).gameObject.transform);
        transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.name = "PendulumPoleLeft";
    }
    void BodyShaftCreate()
    {
        Instantiate(transform.GetChild(0).gameObject.transform.GetChild(0).gameObject, transform.position, new Quaternion(0, 0, 0, 0), transform.GetChild(0).gameObject.transform);
        transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.name = "PendulumBodyShaft";
        transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.localScale = new Vector3(1f, 0.01f, 0.1f);
        transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.AddComponent<Pendulumbody>();
    }

    void CollStop()
    {
        transform.GetChild(0).gameObject.GetComponent<PendulumShaft>().StopFlag = !transform.root.gameObject.GetComponent<PendulumShaft>().StopFlag;
    }
}
