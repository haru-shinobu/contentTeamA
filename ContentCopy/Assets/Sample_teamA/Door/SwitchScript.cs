
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject SwitchHandle;
    void Start()
    {
        tag = "Switch";
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    void TriggerOn()
    {
        SwitchHandle.SendMessage("TriggerOn");
    }
}
