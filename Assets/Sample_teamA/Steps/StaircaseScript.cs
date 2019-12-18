using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaircaseScript : MonoBehaviour
{   
    public int CreateStepNum;//段数。２２．５で一周するイメージ。
    bool Flag = true;
    Vector3 pos;
    private float Timer = 0;
    float R;
    public float RScale;//中心となる場所からの径
    bool CountFlag;

    List<GameObject> stepInstance = new List<GameObject>();
    GameObject InstanceBasicsCube;

    void Start()
    {
        name = "SpiralStep";
        GameObject obj = (GameObject)Resources.Load("SpiralStep");

        pos = transform.position;
        
        R = obj.transform.localScale.z * RScale;
        Timer = 0;
        CountFlag = true;
        
        //        obj.transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");

        Quaternion rotation = gameObject.transform.localRotation;
        Vector3 rotationAngles = rotation.eulerAngles;
        float rot = rotationAngles.y;
        rot = (int)rot;
        gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        for (int num = 0; num < CreateStepNum; num++)
        {
            var PosX = Mathf.Sin(6 * num ) * R;
            var PosY = obj.transform.localScale.y * num;
            var PosZ = -Mathf.Cos(6 * num ) * R;
            Quaternion rote = Quaternion.Euler(4.0f, 16.22f * num , -4.5f);
            GameObject Target = Instantiate(obj, pos + new Vector3(PosX, PosY, PosZ), rote, gameObject.transform);
            Target.transform.gameObject.AddComponent<InstantStaircase>().RootStaircase = gameObject;
            Destroy(Target.gameObject.GetComponent<StaircaseScript>());
            stepInstance.Add(Target);
        }
        gameObject.transform.localRotation = Quaternion.Euler(0, rot, 0);
    }
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
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
        }
        else
        {
            for (int i = (int)Timer + 1; i < CreateStepNum; i++)
                stepInstance[i].gameObject.transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
    public void SwichChange()
    {
        Timer = 0;
        Flag = true;
    }
}