using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StegeMakerScript : MonoBehaviour
{

    //=========================================
    //Sample_TeamAのみのスクリプト
    //=========================================

    /*
    SwitchHandle switch1;
    SwitchHandle switch2;
    bool Flag1;
    bool Flag2;
    int roomcount;
    void Start()
    {
        Flag1 = true;
        Flag2 = true;
        switch1 = GameObject.Find("SwitchHandle").GetComponent<SwitchHandle>();
        switch2 = GameObject.Find("SwitchHandle (1)").GetComponent<SwitchHandle>();
    }
    void a()
    {
        if (switch1.Flag)
            if (Flag1)
                StageMaker1();

        if (switch2.Flag)
            if (Flag2)
                StageMaker2();
    }

    public void StageMaker1()
    {
        GameObject obj = (GameObject)Resources.Load("ShanderiaPoint");
        Instantiate(obj, new Vector3(500, 2000,1300), Quaternion.identity);
        Flag1 = false;
        GameObject.Find("UICanvas").GetComponent<clockScript>().TagnameMemory();
        GameObject.Find("GameMaster").GetComponent<GameStageSetting>().MakeDiv(1);
    }
    public void StageMaker2()
    {
        GameObject obj = (GameObject)Resources.Load("leftchain");
        Instantiate(obj, new Vector3(-321, 1127, -1481), new Quaternion(0, -0.7f, 0, 0.7f));
        obj = (GameObject)Resources.Load("rightchain");
        Instantiate(obj, new Vector3(-332, 1494, -479), new Quaternion(0, 0.7f, 0, 0.7f));
        obj = (GameObject)Resources.Load("onBarrel");
        Instantiate(obj, new Vector3(-417, 1230, -601), Quaternion.identity);
        Instantiate(obj, new Vector3(-414, 863, -1342), Quaternion.identity);
        obj = (GameObject)Resources.Load("Lustre");
        Instantiate(obj, new Vector3(1000, 1600, -1000), Quaternion.identity);
        obj = (GameObject)Resources.Load("dfk_wardrobe_01");
        Instantiate(obj, new Vector3(1380, 1000, -1415), Quaternion.identity);
        obj = (GameObject)Resources.Load("ChainM");
        Instantiate(obj, new Vector3(390, 2483, -540), new Quaternion(1,0,0,0));
        obj = (GameObject)Resources.Load("PushSwitch");
        Instantiate(obj, new Vector3(-486, 700, -540), new Quaternion(0, 0.7f, 0, 0.7f));
        Flag2 = false;
        GameObject.Find("UICanvas").GetComponent<clockScript>().TagnameMemory();
        GameObject.Find("GameMaster").GetComponent<GameStageSetting>().MakeDiv(2);
    }
    public void StageMaker3()
    {
        GameObject obj = (GameObject)Resources.Load("GoalDoor");
        Instantiate(obj, new Vector3(-500, 1000, 20), new Quaternion(0, 0.7f, 0, 0.7f));
        GameObject.Find("GameMaster").GetComponent<GameStageSetting>().MakeDiv(3);
    }
    */
}
