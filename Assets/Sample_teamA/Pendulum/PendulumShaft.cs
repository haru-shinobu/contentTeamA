using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PendulumShaft : MonoBehaviour
{
    Vector3 rot;
    int Count;
    public float Angle;
    public bool StopFlag;
    bool EmittionFlag;
    double [] Anglelist = new double[360];
    void Start()
    {
        rot = transform.rotation.eulerAngles;
        Count = 0;
        StopFlag = false;
        EmittionFlag = false;
        for (int i = 0; i < 360; i++)
        {
            Anglelist[i] = 20 * Math.Sin((2 * Mathf.PI / 180 * i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (!StopFlag)
        {
            Count++;
            if (180 <= Count)
                Count = 0;
            
            Angle = (float)Anglelist[Count];
            transform.eulerAngles = new Vector3(rot.x + Angle, rot.y, rot.z);
        }
    }
    public void StopActivity()
    {
        StopFlag = !StopFlag;
        EmittionFlag = !EmittionFlag;
        if (EmittionFlag)
        {
            gameObject.transform.root.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            gameObject.transform.root.GetChild(1).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }
        else
        {
            gameObject.transform.root.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            gameObject.transform.root.GetChild(1).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
    public void StopTexChange(Texture Tex,bool AbFlag , Color color)
    {
        gameObject.transform.root.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);
        gameObject.transform.root.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);
        gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);


        gameObject.transform.root.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", Tex);
        gameObject.transform.root.GetChild(1).GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", Tex);
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", Tex);
        gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", Tex);

        if (AbFlag)
        {
            gameObject.transform.root.GetChild(1).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }
        else
        {
            gameObject.transform.root.GetChild(1).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }

    }
}
