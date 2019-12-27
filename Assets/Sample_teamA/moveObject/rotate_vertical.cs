using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_vertical : MonoBehaviour
{

    Vector3 pos;
    bool StopFlag = false;
    public bool Flag = false;
    

    void Start()
    {
        Flag = false;
        tag = "Floor&Stop";
        pos = transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.transform.parent == null)
            {
                col.gameObject.transform.SetParent(transform);
                Flag = true;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Flag = false;
            col.gameObject.transform.SetParent(null);
            col.transform.localScale = new Vector3(50, 150, 50);
        }
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (!StopFlag)
        {
            transform.Rotate(new Vector3(0, 0.2f, 0));
        }
        if (Flag)
            if(transform.GetChild(0))
            transform.GetChild(0).gameObject.transform.rotation = transform.rotation;
    }

    void CollReSet()
    {
        transform.Rotate(0,0,0);
        transform.position = pos;
    }
    void CollStop()
    {
        StopFlag = !StopFlag;
    }
}
