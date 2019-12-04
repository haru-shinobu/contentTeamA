using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakpopItemScript : MonoBehaviour
{
    GameObject Instantobj;
    void Start()
    {
        tag = "Untagged";
        foreach (Transform childTransform in gameObject.transform)
            childTransform.tag = "Untagged";
        Instantobj = (GameObject)Resources.Load("monocle");
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Floor")
        {
            Instantiate(Instantobj, transform.position + Vector3.up*100, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
