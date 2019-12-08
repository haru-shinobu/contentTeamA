using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHandle : MonoBehaviour
{
    public GameObject Target;
    public bool Flag = false;
    float rottt = 45;
    float Sub = -2f;
    float targetrotate = 90;
    public bool UseButtonType;
    float ButtonPushmove;
    bool DoorMove;
    void Start()
    {
        Flag = false;
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        tag = "Switch";
        ButtonPushmove = 0;
        DoorMove = false;
    }


    void a()
    {
        if (!UseButtonType)
        {
            if (Flag)
            {
                var Goalrot = Quaternion.Euler(new Vector3(targetrotate + rottt, 0, 0));
                var targetrot = transform.rotation;

                if (Quaternion.Angle(targetrot, Goalrot) <= 1)
                {
                    if (rottt > 0)
                    {
                        Target.GetComponent<Doormove>().switching();
                    }
                    else
                    {
                        Target.GetComponent<Doormove>().CloseSwitch();
                    }

                    transform.rotation = Goalrot;
                    Flag = false;
                    rottt = -rottt;
                }
                else
                {
                    transform.Rotate(new Vector3(Sub, 0, 0));
                }
            }
        }
        else//UseButton
        {
            if (Flag)
            {
                if (0 < Sub)
                {
                    if (ButtonPushmove++ < 5)
                        transform.position = transform.position - transform.forward;
                    else
                        transform.position = transform.position + transform.forward;

                    if (10 <= ButtonPushmove)
                    {
                        Sub = -Sub;
                        DoorMove = !DoorMove;
                        ButtonPushmove = 0;
                    }
                }
                else{
                    if (DoorMove) {
                        Target.GetComponent<Doormove>().switching();
                        Flag = false;
                    }
                    else
                    {
                        Target.GetComponent<Doormove>().CloseSwitch();
                        Flag = false;
                    }
                }
            }
        }
    }
    void TriggerOn()
    {
        if (!Flag)
        {
            Flag = !Flag;
            Sub = -Sub;
        }
    }
}
