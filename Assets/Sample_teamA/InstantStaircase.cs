using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantStaircase : MonoBehaviour
{
    public GameObject RootStaircase;
    void CollStop()
    {
        RootStaircase.SendMessage("CollStop");
    }
}
