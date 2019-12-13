﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPassDiscrimination : MonoBehaviour
{
    public GameObject Target;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            Target.gameObject.GetComponent<Doormove>().ThruPass(true);
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Target.gameObject.GetComponent<Doormove>().Pass();
            Target.gameObject.GetComponent<Doormove>().ThruPass(false);
        }
    }
}
