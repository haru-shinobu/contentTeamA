using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDownFloor : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 Pos;
    public float upLimit;
    private BoxCollider floorCollider;
    bool Flag;
    float time=0;
    bool TimeStopFlag = true;
    public bool MoveFlag = false;
    void Start()
    {
        GameObject WayTrigger;
        WayTrigger = gameObject;

        Vector3 Create = new Vector3(transform.position.x, transform.position.y - transform.localScale.y/2, transform.position.z);
        Instantiate(WayTrigger, Create, Quaternion.identity,transform);
        transform.GetChild(0).gameObject.name = "OneWayBox";
        transform.GetChild(0).gameObject.tag = "Floor&Stop";
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(1,1,1);
        Destroy(transform.GetChild(0).gameObject.GetComponent<MeshRenderer>());
        transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = true;
        Destroy(transform.GetChild(0).gameObject.GetComponent<MoveUpDownFloor>());
        transform.GetChild(0).gameObject.AddComponent<WayTrigger>().call_enter = true;
        transform.GetChild(0).gameObject.GetComponent<WayTrigger>().Parent = transform.gameObject;

        Vector3 CreateParentTriggerMash = new Vector3(transform.position.x, transform.position.y + transform.localScale.y*0.5f, transform.position.z);
        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),CreateParentTriggerMash,Quaternion.identity,transform);
        Destroy(GameObject.Find("Cube"));
        transform.GetChild(1).gameObject.transform.localScale = new Vector3(1,1,1);
        transform.GetChild(1).gameObject.name = "ParentCube";
        Destroy(transform.GetChild(1).gameObject.GetComponent<MeshRenderer>());
        transform.GetChild(1).gameObject.GetComponent<BoxCollider>().isTrigger = true;
        transform.GetChild(1).gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 0.2f, 1);
        Destroy( transform.GetChild(1).gameObject.GetComponent<MeshFilter>());
        transform.GetChild(1).gameObject.AddComponent<ParentMesh>();

        
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().freezeRotation = true;

        rigid = GetComponent<Rigidbody>();
        
        Pos = transform.position;
        floorCollider = GetComponent<BoxCollider>();
        tag = "Floor&Stop";
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") { 
            col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            col.gameObject.GetComponent<PlayerController>().Ground = true;
    }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            floorCollider.isTrigger = false;
            Flag = true;
        }
    }

    private void OnChildTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Flag = false;
            floorCollider.isTrigger = true;
        }
    }

    private void OnChildTriggerEnter_Parent(Collider col)
    {
        if (Flag)
            col.gameObject.transform.SetParent(transform);
    }
    private void OnChildTriggerExit_Parent(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.SetParent(null);
            col.transform.localScale = new Vector3(50, 150, 50);
            col.GetComponent<PlayerController>().Ground = false;
        }
    }

    void Update()
    {
        if(MoveFlag)
        if (TimeStopFlag)
        {
            time += Time.deltaTime * 50;
            if (time > upLimit * 2)
                time = 0;

            rigid.MovePosition(new Vector3(Pos.x, Pos.y + Mathf.PingPong(time, upLimit), Pos.z));
        }
    }

    void CollReSet()
    {
        time = 0;
    }
    void CollStop()
    {
        TimeStopFlag = !TimeStopFlag;
    }
}