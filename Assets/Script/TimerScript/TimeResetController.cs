using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeResetController : MonoBehaviour
{
    void Start()
    {
        Stage1TimeManager.ResetTime();
        Stage2TimeManager.ResetTime();
        Stage3TimeManager.ResetTime();
    }
}
