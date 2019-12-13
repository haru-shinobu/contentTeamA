using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayFloorController : MonoBehaviour
{
    private BoxCollider OneWayCollider;
    Vector3 pos;

    void Start()
    {
        GameObject WayTrigger;
        WayTrigger = gameObject;
        pos = transform.position;
        if (transform.localScale.y <= 5)
            transform.localScale = new Vector3(transform.localScale.x, 6, transform.localScale.z);
        Vector3 Create = new Vector3(transform.position.x, transform.position.y-transform.localScale.y, transform.position.z);
        Instantiate(WayTrigger, Create, Quaternion.identity, transform);
        transform.GetChild(0).gameObject.name = "FloorWayTrigger";
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(1, 1, 1);
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = true;
        Destroy(transform.GetChild(0).gameObject.GetComponent<OneWayFloorController>());
        transform.GetChild(0).gameObject.AddComponent<WayTrigger>().call_enter = true;
        transform.GetChild(0).gameObject.GetComponent<WayTrigger>().Parent = transform.gameObject;

        tag = "Floor";
        OneWayCollider = GetComponent<BoxCollider>();

    }

    private void OnChildTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            OneWayCollider.isTrigger = true;
        }
    }
    

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            OneWayCollider.isTrigger = false;
        }
    }

    void CollReSet()
    {
        transform.position = pos;
        transform.Rotate(0,0,0);
    }

}
