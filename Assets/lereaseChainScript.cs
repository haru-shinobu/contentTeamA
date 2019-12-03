using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lereaseChainScript : MonoBehaviour
{
    GameObject Chain;
    GameObject Picture;
    bool Flag;
    bool ReOpenFlag;
    clockScript textFlag;
    StormScript StormEria;
    ClearLoadScript Clear;
    int count;
    void Start()
    {
        StormEria = GameObject.Find("StormEria").GetComponent<StormScript>();
        StormEria.ActiveFlag = false;
        Chain = GameObject.Find("ChainM(Clone)");
        Picture = GameObject.Find("picture");
        Flag = false;
        ReOpenFlag = false;
        textFlag = GameObject.Find("UICanvas").GetComponent<clockScript>();
        Clear = GameObject.Find("GoalParticleFlag").GetComponent<ClearLoadScript>();
    }

    
    void Update()
    {        
        if(Flag)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Chain.transform.position += Vector3.up * 5;
            if (2900 < Chain.transform.position.y)
            {
                Flag = false;
                Picture.transform.position = new Vector3(765,1180,0);
                Picture.transform.rotation = new Quaternion(-0.3f, 0.7f, 0.3f, 0.7f);
                textFlag.OpenRootFlag = true;
                ReOpenFlag = true;
                StormEria.ActiveFlag = true;
            }
        }
        else
        {
            if (ReOpenFlag)
                if (count++ > 500)
                {
                    ReOpenFlag = false;
                    textFlag.OpenRootFlag = false;
                    Clear.FlagON();
//                    GameObject.Find("GameMaster").GetComponent<GameStageSetting>().MakeDiv(4);
                }
        }        
    }

    void SwichChange()
    {
        Flag = true;
    }
}
