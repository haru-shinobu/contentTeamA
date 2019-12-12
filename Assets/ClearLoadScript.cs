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
        picpos = Picture.transform.position - Vector3.up * 397 + Picture.transform.forward * 352;
        StormEria = GameObject.Find("StormEria").GetComponent<StormScript>();
        StormEria.ActiveFlag = false;
        Clear = GameObject.Find("GoalParticleFlag").GetComponent<ClearLoadScript>();
        Player = GameObject.Find("Player");
        Flag = Flag2 = Flag3 = false;
        obj = GameObject.Find("GameMaster");
        GTD = obj.gameObject.GetComponent<GameTimerDirector>();
        GSS = obj.gameObject.GetComponent<GameStageSetting>();
        textFlag = GameObject.Find("UICanvas").GetComponent<clockScript>();
        if (gameObject.name != "TimeCountStop")
            Flag2 = false;
        else
            Flag2 = true;
        PrefabCanvas = Resources.Load<Canvas>("ClearCanvas" );
        
    }
    void Update() { Debug.Log(Picture.transform.localRotation); }
    void OnTriggerEnter(Collider col)
    {
        if (Flag)
        {
            if (col.gameObject.tag == "Player")
            {
                Flag = false;
                if (gameObject.name != "TimeCountStop")//20/157//1358
                {
                    Picture.transform.position = picpos;
                    //Picture.transform.rotation = new Quaternion(0, 0.9f, 0.5f, 0);
                    Picture.transform.localRotation= new Quaternion(0, 0.5f, 0.9f, 0);
                    textFlag.OpenRootFlag = true;
                    StormEria.ActiveFlag = true;
                    Flag3 = true;
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (Flag3)
            {
                if (count++ > 5)
                {
                    textFlag.OpenRootFlag = false;
                    Flag3 = false;
                }
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
                GameObject.Find("UICanvas").transform.GetChild(13).GetChild(0).gameObject.SetActive(false);
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
    void SwichChange()
    {
        Flag = true;
    }
    public void TriggerOn()
    {
        Flag = true;
    }
}
