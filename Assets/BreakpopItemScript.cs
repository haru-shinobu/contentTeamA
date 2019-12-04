using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakpopItemScript : MonoBehaviour
{
    GameObject Instantobj;
    void Start()
    {
        Instantobj = (GameObject)Resources.Load("monocle");
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Floor")
        {
            Instantiate(Instantobj, transform.position, Quaternion.identity);
        }
    }
}
