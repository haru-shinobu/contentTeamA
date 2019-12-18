using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public GameObject WarldSpacePaper;
    GameObject Player;
    Vector3 pos,scaler;
    Quaternion qua;
    bool moveFlag;
    float mtime;
    void Start()
    {
        moveFlag = false;
        tag = "Switch";
        Player = GameObject.FindWithTag("Player");
        WarldSpacePaper.tag = "BreakItem";
        pos = WarldSpacePaper.transform.position;
        qua = WarldSpacePaper.transform.rotation;
        scaler = WarldSpacePaper.transform.localScale;

        WarldSpacePaper.GetComponent<MeshRenderer>().enabled = false;
        WarldSpacePaper.GetComponent<MeshCollider>().enabled = false;
    }
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (moveFlag)
        {
            mtime = Time.deltaTime * 0.1f;
            WarldSpacePaper.transform.position = Vector3.Lerp(WarldSpacePaper.transform.position, pos, mtime);
            WarldSpacePaper.transform.localScale = Vector3.Lerp(WarldSpacePaper.transform.localScale, scaler, mtime);
            WarldSpacePaper.transform.localRotation = Quaternion.Lerp(WarldSpacePaper.transform.localRotation, qua, mtime);
            if (mtime >= 1)
            {
                moveFlag = false;
                mtime = 0;
            }
        }
    }
    void TriggerOn()
    {
        WarldSpacePaper.transform.position = gameObject.transform.position;
        WarldSpacePaper.transform.localScale = new Vector3(0, 0, 0);
        WarldSpacePaper.transform.rotation = Player.transform.localRotation;
        WarldSpacePaper.GetComponent<MeshRenderer>().enabled = true;
        moveFlag = true;
    }
}
