using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaircaseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject CreateStep;
    
    public int CreateStepNum;//段数。２２．５で一周するイメージ。
    bool Flag = true;
    Vector3 pos;
    private float Timer = 0;
    float R;
    public float RScale;//中心となる場所からの径
    bool CountFlag;

    List<GameObject> stepInstance = new List<GameObject>();

    void Start()
    {
        tag = "Floor&Stop";
        name = "SpiralStep";
        CreateStep = gameObject;
        pos = transform.position + transform.forward * transform.localScale.z * RScale;
        R = transform.localScale.z * RScale;
        Timer = 0;
        CountFlag = true;
        transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");

        for (int num = 1; num < CreateStepNum + 1; num++)
        {
            var PosX = Mathf.Sin(6 * num) * R;
            var PosY = transform.localScale.y * num;
            var PosZ = -Mathf.Cos(6 * num) * R;
            Quaternion rote = Quaternion.Euler(0.0f, 16.22f * num, -3.0f);
            GameObject Target = Instantiate(CreateStep, pos + new Vector3(PosX, PosY, PosZ), rote);
            Target.transform.gameObject.AddComponent<InstantStaircase>().RootStaircase = gameObject;
            Destroy(Target.gameObject.GetComponent<StaircaseScript>());
            stepInstance.Add(Target);
        }
    }
    void Update()
    {
        if (Flag)
        {
            for (int num = 0; num < CreateStepNum; num++)
            {
                stepInstance[num].GetComponent<MeshRenderer>().enabled = true;
                stepInstance[num].GetComponent<BoxCollider>().enabled = true;
            }
            Flag = false;
        }

        if (CountFlag)
            Timer += Time.deltaTime;
        if (Timer < CreateStepNum)
        {
            stepInstance[(int)Timer].GetComponent<MeshRenderer>().enabled = false;
            stepInstance[(int)Timer].GetComponent<BoxCollider>().enabled = false;
        }
        if (CreateStepNum + 5 < Timer)
        {
            Timer = 0;
            Flag = true;
        }
    }

    void CollStop()
    {
        CountFlag = !CountFlag;
        if (!CountFlag)
        {
            for (int i = (int)Timer + 1; i < CreateStepNum; i++)
                stepInstance[i].gameObject.transform.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            transform.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }
        else
        {
            for (int i = (int)Timer + 1; i < CreateStepNum; i++)
                stepInstance[i].gameObject.transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
}