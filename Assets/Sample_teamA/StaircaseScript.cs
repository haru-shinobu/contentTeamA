using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaircaseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject CreateStep;
    [SerializeField]
    private int CreateStepNum;//段数。２２．５で一周するイメージ。
    private int Angle = 6;
    bool Flag = true;
    Vector3 pos;
    private float Timer = 0;
    float R;
    public float RScale;//中心となる場所からの径
    void Start()
    {
        tag = "Floor";
        CreateStep = gameObject;
        pos = transform.position + transform.forward * transform.localScale.z * RScale;
        R = transform.localScale.z * RScale;
        Timer = 0;
    }
    void Update() { 
        if (Flag)
        {
            Flag = false;
            for (int num = 1; num < CreateStepNum; num++)
            {
                var PosX = Mathf.Sin(6 * num) * R;
                var PosY = transform.localScale.y * num;
                var PosZ = -Mathf.Cos(6 * num) * R;
                Quaternion rote = Quaternion.Euler(0.0f, 16.22f*num, -3.0f);
                GameObject Target = Instantiate(CreateStep, pos + new Vector3(PosX, PosY, PosZ), rote);
                Target.transform.gameObject.AddComponent<InstantStaircase>().DestroyTimer = num;
                Destroy(Target.gameObject.GetComponent<StaircaseScript>());
                Target.name = "SpiralStep";
            }
        }
        Timer += Time.deltaTime;
        if(CreateStepNum + 5 < Timer)
        {
            Timer = 0;
            Flag = true;
        }
    }
}
