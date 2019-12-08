using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakpopItemScript : MonoBehaviour
{
    GameObject Instantobj;
    private bool flag;
    void Start()
    {
        tag = "BreakItem";//"Untagged";
        foreach (Transform childTransform in gameObject.transform)
            childTransform.tag = "BreakItem";//"Untagged";
        Instantobj = (GameObject)Resources.Load("monocle");
        flag = true;
    }


    void OnTriggerEnter(Collider col)
    {
        if (!(col.tag == "Player" || col.tag == "Break&Wall"))
        {
            if (!flag)
                Instantiate(Instantobj, transform.position + Vector3.up * 100, Quaternion.identity);
            flag = false;
            Destroy(gameObject);
        }
    }
}
