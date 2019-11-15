using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float StormForce;
    void Start()
    {
        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
            col.gameObject.GetComponent<Rigidbody>().AddForce(transform.right*StormForce,ForceMode.Impulse);
    }
}
