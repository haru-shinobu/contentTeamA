using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerMoving : MonoBehaviour
{
    Vector3  SceneEndPos;
    Rigidbody rb;
    void Start()
    {
        SceneEndPos = GameObject.Find("GoalwatchPointer").transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        float ANum = Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, SceneEndPos, ANum*0.05f);
        rb.AddForce(-Vector3.up, ForceMode.Force);
    }
}
