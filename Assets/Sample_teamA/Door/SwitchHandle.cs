using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHandle : MonoBehaviour
{
    public GameObject Target;
    public GameObject Target2;
    bool Flag = false;
    float rottt = 45;
    float Sub = -2f;
    float targetrotate = 90;
    
    void Start()
    {
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        tag = "Switch";
    }

    
    void Update()
    {
     
        if (Flag)
        {
            var Goalrot = Quaternion.Euler(new Vector3(targetrotate + rottt, 0, 0));
            var targetrot = transform.rotation;            

            if (Quaternion.Angle(targetrot,Goalrot) <= 1)
            {
                if (rottt > 0)
                {
                    Target.GetComponent<Doormove>().switching();
                    Target2.GetComponent<Doormove>().switching();
                }
                else
                {
                    Target.GetComponent<Doormove>().CloseSwitch();
                    Target2.GetComponent<Doormove>().CloseSwitch();
                }
                
                transform.rotation = Goalrot;
                Flag = false;
                rottt = -rottt;
            }
            else
            {
                transform.Rotate(new Vector3(Sub, 0,0));
            }
        }
    }
    
    void TriggerOn()
    {
        Flag = !Flag;
        Sub = -Sub;
    }
}
