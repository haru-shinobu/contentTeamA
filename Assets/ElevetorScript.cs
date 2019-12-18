using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevetorScript : MonoBehaviour
{
    private BoxCollider floorCollider;
    Vector3 Pos;
    bool TimeStopFlag = true;
    public float upLimit;
    float time = 0;
    void Start()
    {
        transform.GetChild(0).gameObject.AddComponent<WayTrigger>().call_enter = true;
        transform.GetChild(0).gameObject.GetComponent<WayTrigger>().Parent = transform.gameObject;

        Pos = transform.position;
        floorCollider = GetComponent<BoxCollider>();
        tag = "Floor&Stop";
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.SetParent(transform);
            col.gameObject.GetComponent<PlayerController>().Ground = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.SetParent(null);
            floorCollider.isTrigger = false;
        }
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (TimeStopFlag)
        {
            time += Time.deltaTime * 50;
            if (time > upLimit * 2)
                time = 0;

            transform.position = (new Vector3(Pos.x, Pos.y + Mathf.PingPong(time, upLimit), Pos.z));
        }
    }
    void CollStop()
    {
        TimeStopFlag = !TimeStopFlag;
    }

    private void OnChildTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            floorCollider.isTrigger = true;
        }
    }    
}
