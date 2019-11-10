using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStaircaseScript : MonoBehaviour
{
    private GameObject CreateStep;
    public bool Flag;
    public int CreateStepNum; //階段の長さ（段数）
    //numは(段数+1)*2となる。注意。
    void Start()
    {
        Flag = true;
        transform.tag = "Floor";
        CreateStep = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag)
            Create();
    }

    void Create()
    {
        Flag = false;
        int CreateNum = CreateStepNum;
        //底板
        Vector3 pos = transform.position - transform.forward * transform.localScale.z + transform.up * transform.localScale.y;
        Instantiate(CreateStep, pos, Quaternion.identity, transform);
        Destroy(transform.GetChild(0).GetComponent<RotateStaircaseScript>());
        transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
        transform.GetChild(0).name = "Step";
        GameObject child = gameObject;
        int num = 0;
        while (0 < CreateNum)
        {
            pos = transform.GetChild(num).transform.position - transform.forward * transform.localScale.z + transform.up * transform.localScale.y;
            Instantiate(transform.GetChild(0).gameObject, pos, Quaternion.identity, transform);
            num++;
            CreateNum--;
        }
        //欄干の柱
        pos = transform.position + transform.up * transform.localScale.y * 5 + transform.right * transform.localScale.x * 0.5f;
        Instantiate(transform.GetChild(num), pos, Quaternion.identity, transform);
        Destroy(transform.GetChild(num + 1).GetComponent<RotateStaircaseScript>());
        transform.GetChild(num + 1).transform.localScale = new Vector3(0.05f, 10, 0.2f);

        pos = transform.position + transform.up * transform.localScale.y * 5 - transform.right * transform.localScale.x * 0.5f;
        Instantiate(transform.GetChild(num), pos, Quaternion.identity, transform.GetChild(num + 1).transform);

        CreateNum = CreateStepNum + 1;
        while (0 < CreateNum)
        {
            pos = transform.GetChild(num + 1).transform.position - transform.forward * transform.localScale.z + transform.up * transform.localScale.y;
            Instantiate(transform.GetChild(num + 1).gameObject, pos, Quaternion.identity, transform);
            num++;
            CreateNum--;
        }
        //欄干
        num -= CreateStepNum + 1;
        CreateNum = CreateStepNum + 2;
        while (0 < CreateNum)
        {
            pos = transform.GetChild(num + 1).transform.position + transform.up * transform.GetChild(num + 1).transform.lossyScale.y * 0.5f;
            Instantiate(transform.GetChild(num + 1).gameObject, pos, Quaternion.identity, transform.GetChild(num + 1));
            transform.GetChild(num + 1).gameObject.transform.GetChild(1).transform.localScale = new Vector3(1, 0.1f, 5);
            transform.GetChild(num + 1).gameObject.transform.GetChild(1).transform.rotation = Quaternion.Euler(1.15f, 0, 0);
            num++;
            CreateNum--;
        }
    }
}