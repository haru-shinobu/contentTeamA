
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    GameObject SwitchButton;
    void Start()
    {
        tag = "Switch";
        SwitchButton = gameObject.transform.root.GetChild(0).gameObject;
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    void TriggerOn()
    {
        SwitchButton.SendMessage("TriggerOn");
    }
}
