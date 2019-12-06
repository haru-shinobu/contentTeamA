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
    Material mat;
    void Start()
    {
        tag = "Switch";
        StartPos = gameObject.transform.localPosition;
        Flag = false;
        ButtonPushmove = 0;
        mat = gameObject.GetComponent<MeshRenderer>().material;
        mat.DisableKeyword("_EMISSION");
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
                Target.SendMessage("SwichChange");
                mat.DisableKeyword("_EMISSION");
                //ChainMから外したので後で再ターゲット？？
            }
        }
    }

    void TriggerOn()
    {
        Sub = -Sub;
        mat.EnableKeyword("_EMISSION");
        Flag = true;
    }
}
