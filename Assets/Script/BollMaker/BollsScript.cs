using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollsScript : MonoBehaviour
{
    Rigidbody rb;
    Vector3 pos;
    Vector3 ni;
    bool flag;
    void Start()
    {
        rb = gameObject.transform.GetComponent<Rigidbody>();
        flag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!flag) {
            float sousand = 1000;
            rb.AddTorque(gameObject.transform.forward * sousand * sousand * 10, ForceMode.Impulse);
            rb.AddForce(-Vector3.up * sousand * sousand, ForceMode.Impulse);
            pos = transform.position;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Floor" || col.gameObject.tag == "wall")
        {
            gameObject.transform.parent.SendMessage("Del");
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            float posy = col.transform.position.y;
            col.transform.position += transform.position - pos - transform.localScale*0.9f;
            col.transform.position = new Vector3(col.transform.position.x, posy, col.transform.position.z);
        }
    }

    void CollStop()
    {
        gameObject.transform.parent.gameObject.SendMessage("CollStop");
        if (!flag)
        {
            ni = rb.velocity;
            rb.velocity *= 0;
            rb.isKinematic = true;
            flag = true;
        }
        else
        {
            rb.velocity = ni;
            rb.isKinematic = false;
            flag = false;
        }
    }
}
