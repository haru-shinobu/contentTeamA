using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStaircaseScript : MonoBehaviour
{
    private GameObject CreateStep;
    private Quaternion away;
    public bool BridgeFlag;
    public bool CreateFlag;
    bool Flag;    
    public int CreateStepNum; //階段の長さ（段数）
    int Steps;                          //numは(段数+1)*2となる。注意。
    int Childnum;
    void Start()
    {
        CreateFlag = false;
        Flag = true;
        Childnum = 0;
        CreateStep = gameObject;
        away = transform.localRotation;        
        Steps = CreateStepNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateFlag)
            if (Flag)
            {
                transform.localRotation = new Quaternion(0, 0, 0, 0);
                if (BridgeFlag)
                {
                    Create(false);
                    gameObject.transform.DetachChildren();
                    BridgeFlag = false;
                    transform.localRotation = new Quaternion(0, 0, 0, 0);
                }
                else
                    Create(true);
            }
    }

    void Create(bool Pass)
    {
        if (!Pass)
        {
            
            Steps /= 2;
            Steps -= 2;
        }
        else
        {
            Flag = false;
            if(BridgeFlag)
            {
                Steps = CreateStepNum / 2 - 2;
            }
        }
        int CreateNum = Steps;
        
        //底板
        Vector3 pos;
        if (Pass)
            pos = transform.position - transform.forward * transform.localScale.z + transform.up * transform.localScale.y;
        else
        {
            pos = transform.position + transform.forward * transform.localScale.z * (Steps + 1.5f) * 2;// + transform.up * transform.localScale.y;
        }

        Instantiate(CreateStep, pos, Quaternion.identity, transform);

        Destroy(transform.GetChild(Childnum).GetComponent<RotateStaircaseScript>());
        transform.GetChild(Childnum).transform.localScale = new Vector3(1, 1, 1);
        transform.GetChild(Childnum).name = "Step";
        GameObject child = gameObject;
        int num = 0;
        if (!Pass)
            CreateNum++;
        while (0 < CreateNum)
        {
            if (Pass)
                pos = transform.GetChild(Childnum+num).transform.position - transform.forward * transform.localScale.z + transform.up * transform.localScale.y;
            else
                pos = transform.GetChild(Childnum+num).transform.position - transform.forward * transform.localScale.z + transform.up * transform.localScale.y;
            Instantiate(transform.GetChild(Childnum).gameObject, pos, Quaternion.identity, transform);
            num++;
            CreateNum--;
        }
        
        //欄干の柱
        if(Pass)
        pos = transform.position + transform.up * transform.localScale.y * 5 + transform.right * transform.localScale.x * 0.5f;
        else
            pos = transform.position + transform.forward * transform.localScale.z * (Steps + 1.5f) * 2 + transform.up * transform.localScale.y * 5 + transform.right * transform.localScale.x * 0.5f;
        
        Instantiate(transform.GetChild(Childnum+num), pos, Quaternion.identity, transform);
        Destroy(transform.GetChild(Childnum+num + 1).GetComponent<RotateStaircaseScript>());
        transform.GetChild(Childnum+num + 1).transform.localScale = transform.right * transform.GetChild(Childnum+num + 1).transform.localScale.x * 0.05f + transform.up * transform.GetChild(num + 1).transform.localScale.y * 10 + transform.forward * transform.GetChild(num + 1).transform.localScale.z * 0.2f;

        if (Pass)
            pos = transform.position + transform.up * transform.localScale.y * 5 - transform.right * transform.localScale.x * 0.5f;
        else
            pos = transform.position + transform.forward * transform.localScale.z * (Steps + 1.5f) * 2+ transform.up * transform.localScale.y * 5 - transform.right * transform.localScale.x * 0.5f;
        Instantiate(transform.GetChild(Childnum+num), pos, Quaternion.identity, transform.GetChild(Childnum+num + 1).transform);

        CreateNum = Steps + 1;
        while (0 < CreateNum)
        {
            if (Pass)
                pos = transform.GetChild(Childnum+num + 1).transform.position - transform.forward * transform.localScale.z + transform.up * transform.localScale.y;
            else
                pos = transform.GetChild(Childnum+num + 1).transform.position - transform.forward * transform.localScale.z + transform.up * transform.localScale.y;
            Instantiate(transform.GetChild(Childnum+num + 1).gameObject, pos, Quaternion.identity, transform);
            num++;
            CreateNum--;
        }
        //欄干
        num -= Steps + 1;
        CreateNum = Steps + 2;

        while (0 < CreateNum)
        {
            pos = transform.GetChild(Childnum+num + 1).transform.position + transform.up * transform.GetChild(Childnum+num + 1).transform.lossyScale.y * 0.5f;
            Instantiate(transform.GetChild(Childnum + num + 1).gameObject, pos, Quaternion.identity, transform.GetChild(Childnum+num + 1));
            transform.GetChild(Childnum+num + 1).gameObject.transform.GetChild(1).transform.localScale = transform.right * 1 + transform.up * 0.1f + transform.forward * 5;
            transform.GetChild(Childnum+num + 1).gameObject.transform.GetChild(1).transform.rotation = Quaternion.Euler(1.15f, 0, 0);
            num++;
            CreateNum--;
        }

        if (Pass)
        {
            transform.localRotation = away;
            BridgeFlag = false;
        }
        else
            transform.localRotation = new Quaternion(0, 1, 0, 1);
        Childnum = 0;//num  +1;
    }

    void SwichChange()
    {
        CreateFlag = true;
    }
        
}