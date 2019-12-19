using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_horizonal : MonoBehaviour
{
    Rigidbody rigid;
    Vector3 pos;
    bool Flag = false;
    void Start()
    {
        pos = transform.position;
        tag = "Floor&Stop";
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        rigid = gameObject.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (!Flag)
            transform.Rotate(new Vector3(1, 0, 0));
    }


    void CollReSet()
    {
        rigid.transform.Rotate(0, 0, 0);
        transform.position = pos;
    }
    void CollStop()
    {
        Flag = !Flag;
    }
}
