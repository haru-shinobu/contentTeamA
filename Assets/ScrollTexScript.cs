using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexScript : MonoBehaviour
{
    public GameObject WarldSpaceText;
    GameObject Player;
    Vector3 pos, scaler;
    Quaternion qua;
    bool moveFlag;
    float mtime;
    void Start()
    {
        moveFlag = false;
        tag = "Switch";
        Player = GameObject.FindWithTag("Player");
        WarldSpaceText.tag = "BreakItem";
        pos = WarldSpaceText.transform.position;
        qua = WarldSpaceText.transform.localRotation;
        scaler = WarldSpaceText.transform.localScale;

        WarldSpaceText.GetComponent<Canvas>().enabled = false;
    }
    void Update()
    {
        if (moveFlag)
        {
            mtime = Time.deltaTime;
            WarldSpaceText.transform.position = Vector3.Lerp(WarldSpaceText.transform.position, pos, mtime);
            WarldSpaceText.transform.localScale = Vector3.Lerp(WarldSpaceText.transform.localScale, scaler, mtime);
            WarldSpaceText.transform.localRotation = Quaternion.Lerp(WarldSpaceText.transform.localRotation, qua, mtime);
            if (mtime >= 1)
            {
                moveFlag = false;
                mtime = 0;
            }
        }
    }
    void TriggerOn()
    {
        WarldSpaceText.transform.position = gameObject.transform.position;
        WarldSpaceText.transform.localScale = new Vector3(0, 0, 0);
        WarldSpaceText.transform.rotation = Player.transform.localRotation;
        WarldSpaceText.GetComponent<Canvas>().enabled = true;
        moveFlag = true;
    }
    
}
