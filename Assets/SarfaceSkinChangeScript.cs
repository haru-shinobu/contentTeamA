using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarfaceSkinChangeScript : MonoBehaviour
{
    List<Material> _MatNumList;
    NumberSwitchScript script;
    private int Number;
    MeshRenderer mat;
    private int index;
    private int Num;
   

    void Start()
    {
        tag = "Switch";
        script = gameObject.transform.parent.GetComponent<NumberSwitchScript>();
        switch (gameObject.name)
        {
            case "NumberPlate1":
                Number = script.HitNumber1;
                break;
            case "NumberPlate2":
                Number = script.HitNumber2;
                break;
            case "NumberPlate3":
                Number = script.HitNumber3;
                break;
        }
        Num = 0;


        _MatNumList = script._MatNumList;
        mat = gameObject.GetComponent<MeshRenderer>();
        index = Random.Range(0,6);
        while (index == Number)
            index = Random.Range(0, 6);
        mat.material = _MatNumList[index];
        
    }
    void TriggerOn()
    {
        if (script.LockOpen())
        {
            Num++;
            if (6 <= Num + index)
                Num -= 6;
            mat.material = _MatNumList[Num + index];
            script.HitCheck(gameObject.name, Num + index);                
        }
    }
}
