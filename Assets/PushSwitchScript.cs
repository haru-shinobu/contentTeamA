using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSwitchScript : MonoBehaviour
{
    Vector3 StartPos;
    bool Flag;
    public GameObject Target;
    float ButtonPushmove;
    float Sub = -2f;
    void Start()
    {
        tag = "Switch";
        StartPos = gameObject.transform.position;
        Flag = false;
        ButtonPushmove = 0;
        Target = GameObject.Find("ChainM");
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag)
        {
            if (0 < Sub)
            {
                if (ButtonPushmove++ < 5)
                    transform.localPosition -= transform.up;
                else
                    transform.localPosition += transform.up;

                if (10 <= ButtonPushmove)
                {
                    Sub = -Sub;
                    ButtonPushmove = 0;
                }
            }
            else
            {
                Flag = false;
                GameObject.Find("WeightNet").SendMessage("SwichChange");
            }
        }
    }

    void TriggerOn()
    {
        Flag = true;
    }
}
