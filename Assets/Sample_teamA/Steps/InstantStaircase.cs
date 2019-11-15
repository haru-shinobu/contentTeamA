using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantStaircase : MonoBehaviour
{
    public GameObject RootStaircase;
    void Start()
    {
        gameObject.transform.localScale = new Vector3(75, 10, 200);
    }
    void CollStop()
    {
        RootStaircase.SendMessage("CollStop");
    }
}
