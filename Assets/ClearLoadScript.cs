using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearLoadScript : MonoBehaviour
{
    bool Flag;
    bool Flag2;
    GameTimerDirector GTD;
    GameStageSetting GSS;
    GameObject Player;
    GameObject GoalEffect;
    GameObject obj;
    public GameObject sulewall;
    Canvas PrefabCanvas;
    void Start()
    {

        GoalEffect = GameObject.Find("GoalEffect");
        Player = GameObject.Find("Player");
        Flag = false;
        Flag2 = false;
        obj = GameObject.Find("GameMaster");
        GTD = obj.gameObject.GetComponent<GameTimerDirector>();
        GSS = obj.gameObject.GetComponent<GameStageSetting>();

        if (gameObject.name == "GoalParticleFlag")
            Flag2 = false;
        else
            Flag2 = true;
        PrefabCanvas = Resources.Load<Canvas>("ClearCanvas" );
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (Flag)
        {
            if (col.gameObject.tag == "Player")
            {
                Flag = false;
               GameObject.Find("GameObjectMaker").gameObject.GetComponent<StegeMakerScript>().StageMaker3();
                if (gameObject.name == "GoalParticleFlag")
                    Destroy(gameObject);
            }
        }
        if (Flag2)
            if (col.gameObject.tag == "Player")
            {
                sulewall.transform.GetChild(0).GetComponent<MeshCollider>().enabled = false;
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
}
