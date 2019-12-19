using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulumbody : MonoBehaviour
{
    GameObject Shaft;
    Vector3 targetPos, BodyPos;
    float High;

    void Start()
    {
        tag = "Floor&Stop";
        Destroy(gameObject.GetComponent<Pendulum>());
        gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition |
        RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Shaft = transform.parent.gameObject;

        Instantiate(gameObject, transform.position, new Quaternion(0, 0, 0, 0), transform.root.gameObject.transform);
        Destroy(transform.root.GetChild(1).gameObject.transform.GetComponent<Pendulumbody>());
        Destroy(transform.root.GetChild(1).gameObject.transform.GetComponent<Pendulum>());
        Destroy(transform.root.GetChild(1).gameObject.transform.GetComponent<Rigidbody>());
        transform.root.GetChild(1).gameObject.transform.GetComponent<BoxCollider>().enabled = true;
        transform.root.GetChild(1).gameObject.transform.GetComponent<BoxCollider>().isTrigger = true;
        float bodylong = transform.root.GetComponent<Pendulum>().PendulumLong;
        transform.root.GetChild(1).gameObject.transform.localScale = new Vector3(1f, 0.5f, bodylong);
        transform.root.GetChild(1).gameObject.transform.GetComponent<BoxCollider>();
        transform.root.GetChild(1).gameObject.transform.name = "PendulumBody";
        transform.root.GetChild(1).gameObject.AddComponent<Pendulumbodyfloot>();

        gameObject.GetComponent<MeshRenderer>().enabled = false;

        High = transform.root.GetComponent<Pendulum>().PendulumHigh;
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        targetPos = Shaft.transform.position;
        transform.position = targetPos - Shaft.transform.up * (High*0.8f);
    }
}
