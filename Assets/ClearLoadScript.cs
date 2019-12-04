using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearLoadScript : MonoBehaviour
{
    public bool Flag, Flag2,Flag3;
    GameTimerDirector GTD;
    GameStageSetting GSS;
    GameObject Player , Picture , obj;
    public GameObject sulewall;
    Canvas PrefabCanvas;
    StormScript StormEria;
    ClearLoadScript Clear;
    clockScript textFlag;
    Vector3 picpos;
    float count;
    ParticleSystem Particle;
    void Start()
    {
        Particle = GameObject.Find("GoalEffect").GetComponent<ParticleSystem>();
        Particle.Stop();
        count = 0;
        Picture = GameObject.Find("picture");
        picpos = Picture.transform.position;
        StormEria = GameObject.Find("StormEria").GetComponent<StormScript>();
        StormEria.ActiveFlag = false;
        Clear = GameObject.Find("GoalParticleFlag").GetComponent<ClearLoadScript>();
        Player = GameObject.Find("Player");
        Flag = Flag2 = Flag3 = false;
        obj = GameObject.Find("GameMaster");
        GTD = obj.gameObject.GetComponent<GameTimerDirector>();
        GSS = obj.gameObject.GetComponent<GameStageSetting>();
        textFlag = GameObject.Find("UICanvas").GetComponent<clockScript>();
 //       if (gameObject.name != "TimeCountStop")
 //           Flag2 = false;
 //       else
 //           Flag2 = true;
        PrefabCanvas = Resources.Load<Canvas>("ClearCanvas" );
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (Flag)
        {
            if (col.gameObject.tag == "Player")
            {
                Flag = false;
                if (gameObject.name != "TimeCountStop")
                {
                    Picture.transform.position = picpos - Picture.transform.up * 374 + Picture.transform.forward * 255;
                    Picture.transform.rotation = Quaternion.Euler(-45, 180, 0);

                    textFlag.OpenRootFlag = true;
                    StormEria.ActiveFlag = true;
                    Clear.FlagON();
                    Destroy(gameObject);
                    Flag3 = true;
                }
            }
        }
        else
        {
            if (Flag3)
                if (count++ > 500)
                {
                    textFlag.OpenRootFlag = false;
                    Flag3 = false;
                    Flag2 = true;
                }
        }
        
        if (Flag2)
            if (col.gameObject.tag == "Player")
            {
                Particle.Play();
                if (sulewall.transform.GetChild(0).GetComponent<MeshCollider>()) {
                        sulewall.transform.GetChild(0).GetComponent<MeshCollider>().enabled = false;
                }
                GTD.GameCrearLoadFlag = true;
                GSS.ClearTimeStop();
                GSS.ClearLoad();
                Player.GetComponent<PlayerController>().enabled = false;
                Player.AddComponent<ClearPlayerMoving>();
                Destroy(GameObject.Find("footCanvas"));
                GameObject.Find("FPSCamera").GetComponent<FPSCameraController>().CamControllFlag = false;
                GameObject.Find("UICanvas").transform.GetChild(12).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("UICanvas").GetComponent<Canvas>().enabled = false;
                obj.GetComponent<CursorColtroll>().enabled = false;
                obj.GetComponent<RayAbility>().enabled = false;
                Destroy(GameObject.Find("CursolCanvas"));

                Instantiate(PrefabCanvas);
            }
    }
    public void FlagON()
    {
        Flag = true;
    }
    public void TriggerOn()
    {
        Flag = true;
    }
}
