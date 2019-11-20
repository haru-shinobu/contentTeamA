﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollsScript : MonoBehaviour
{
    Rigidbody rb;
    Vector3 pos;
    Vector3 ni;
    void Start()
    {
        rb = gameObject.transform.GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float sousand = 1000;
        rb.AddTorque(gameObject.transform.forward * sousand* sousand * 10, ForceMode.Impulse);
        rb.AddForce(-Vector3.up * sousand* sousand, ForceMode.Impulse);
        pos = transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Floor" || col.gameObject.tag == "wall")
        {
            gameObject.transform.root.SendMessage("Del");
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.position += transform.position - pos - transform.localScale*0.9f;
        }
    }

    void CollStop()
    {
        if (rb.velocity.x != 0 && rb.velocity.y != 0 && rb.velocity.z != 0)
        {
            ni = rb.velocity;
            rb.velocity *= 0;
        }
        else
        {
            rb.velocity = ni;
        }
    }
}