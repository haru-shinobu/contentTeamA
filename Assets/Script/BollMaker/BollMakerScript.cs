using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollMakerScript : MonoBehaviour
{
    //public int BollNum;
    public int ReCastTime;
    public float bollScale;

    float TimeCountB;
    bool Flag;
    bool RemakeFlag;

    void Start()
    {
        gameObject.tag = "move&Stop";
        TimeCountB = 0;
        RemakeFlag = true;
        if (ReCastTime <= 6)
            ReCastTime = 6;
        if (bollScale < 1)
            bollScale = 1;
        Flag = false;
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (Flag) {
            if (RemakeFlag)
                TimeCountB += Time.deltaTime;
            if (ReCastTime <= TimeCountB) {
                TimeCountB -= ReCastTime;
                Maker();
            }
        }
    }

    void CollStop()
    {
        Flag = !Flag;
    }
    void SwichChange(){
        Flag = true;
    }
    
    void Maker()
    {
        GameObject obj = (GameObject)Resources.Load("Boll");
        GameObject Boll = Instantiate(obj, transform.position, Quaternion.identity, transform);
        Boll.transform.localScale = new Vector3(bollScale,bollScale,bollScale);
        RemakeFlag = false;
    }
    void Del()
    {
        RemakeFlag = true;
    }
}
