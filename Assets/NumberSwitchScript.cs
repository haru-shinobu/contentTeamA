using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumberSwitchScript : MonoBehaviour
{
    GameObject NumSarface1;
    GameObject NumSarface2;
    GameObject NumSarface3;

    public int HitNumber1;
    public int HitNumber2;
    public int HitNumber3;

    public GameObject Target;
    private int[] LockPass = { 0, 0, 0 };

    public List<Material> _MatNumList;
    TextMesh tex;

    void Start()
    {   
        NumSarface1 = gameObject.transform.GetChild(0).gameObject;
        NumSarface2 = gameObject.transform.GetChild(1).gameObject;
        NumSarface3 = gameObject.transform.GetChild(2).gameObject;
        tex = gameObject.transform.GetChild(4).GetComponent<TextMesh>();
        tex.text = "CLOSE";
    }

    public void HitCheck(string name, int val)
    {
        if (NumSarface1.name == name)
        {
            if (HitNumber1 == val)
                LockPass[0] = 1;
            else
                LockPass[0] = 0;
        }
        else
        if (NumSarface2.name == name)
        {
            if (HitNumber2 == val)
                LockPass[1] = 1;
            else
                LockPass[1] = 0;
        }
        else
        if (NumSarface3.name == name)
            if (HitNumber3 == val)
                LockPass[2] = 1;
            else
                LockPass[2] = 0;
        if (LockPass[0] == 1 && LockPass[1] == 1 && LockPass[2] == 1)
        {
            tex.text = "OPEN";
            if (Target)
                Target.SendMessage("TriggerOn");
            else
                tex.text = "無駄無駄ぁ！つながっていない！";
        }
        else
        {
            tex.text = "ERROR";
        }
    }
}