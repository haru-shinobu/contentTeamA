using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantStaircase : MonoBehaviour
{
    public float DestroyTimer;

    void Update()
    {
        DestroyTimer -= Time.deltaTime;
        if (DestroyTimer < 0)
            Destroy(this.gameObject);
    }
}
