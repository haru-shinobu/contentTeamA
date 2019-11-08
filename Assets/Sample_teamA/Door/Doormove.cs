using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doormove : MonoBehaviour
{
    
    bool OpenFlag;
    bool CloseFlag;
    bool DoorWay = true;
    float rot;
    int Timer;
    Vector3 pos;
    Vector3 NowPos;
    Vector3 FixRot;

    GameObject DoorCollide;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        OpenFlag = false;
        CloseFlag = false;
        rot = transform.rotation.y;
        if (transform.tag == "door_right")
            pos = transform.position + new Vector3(transform.localScale.x * 0.5f, 0, 0);
        if (transform.tag == "door_left")
        {
            pos = transform.position + new Vector3(-transform.localScale.x * 0.5f, 0, 0);
            DoorWay = false;            
        }
        NowPos = transform.position;
        FixRot = transform.localRotation.eulerAngles;
        
        DoorCollide = Instantiate(transform.gameObject,transform.position,new Quaternion(0,0,0,0));
        DoorCollide.transform.name = "Door";
        DoorCollide.gameObject.GetComponent<MeshRenderer>().enabled = false;
        DoorCollide.gameObject.transform.GetComponent<BoxCollider>().isTrigger = true;
        DoorCollide.transform.localScale = transform.localScale;
        Destroy(DoorCollide.transform.GetComponent<Doormove>());
        DoorCollide.transform.gameObject.AddComponent<DoorPassDiscrimination>().Target = gameObject;
    }

    
    void Update()
    {

        int speed = 50;

        if (OpenFlag)
        {
            if (DoorWay)//trueのとき右扉動作
                if (transform.rotation.y <= 0.7f)
                    transform.RotateAround(pos, transform.up, Time.deltaTime * speed);
                else
                    OpenFlag = !OpenFlag;
            if (!DoorWay)
                if (-0.7f <= transform.rotation.y)
                {
                    transform.RotateAround(pos, transform.up, -Time.deltaTime * speed);
                }
                else
                    OpenFlag = !OpenFlag;
        }
        if (CloseFlag)
        {
            if (DoorWay)//trueのとき右扉動作
                if (0 < transform.rotation.y)
                    transform.RotateAround(pos, transform.up, -Time.deltaTime * speed);
                else
                    CloseFlag = !CloseFlag;
            if (!DoorWay)
                if (transform.rotation.y < 0)
                    transform.RotateAround(pos, transform.up, Time.deltaTime * speed);
                else
                    CloseFlag = !CloseFlag;
        }

        if (0 < Timer)
        {
            Timer--;
            if (Timer <= 0)
                CloseSwitch();
        }
    }
    public void switching()
    {
        OpenFlag = !OpenFlag;
    }
    public void CloseSwitch()
    {
        CloseFlag = !CloseFlag;
    }
    public void Pass()
    {
        Timer = 90;
    }
}