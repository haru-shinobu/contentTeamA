using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamWayScript : MonoBehaviour
{
    void OnBecameInvisible()
    {
        enabled = false;
    }
    void OnBecameVisible()
    {
        enabled = true;
    }
}
