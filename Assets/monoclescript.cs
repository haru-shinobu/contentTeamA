using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monoclescript : MonoBehaviour
{
    GameObject FPSCamera;
    RayAbility Rays;
    Vector3 pos;
    private bool Flag;
    float time;
    int Limit;
    GameObject Instant;
    Vector3 Scal;
    Transform trans;
    bool FallFlag;
    void Start()
    {
        Flag = false;
        FallFlag = true;
        FPSCamera = GameObject.Find("FPSCamera");
        Rays = GameObject.Find("GameMaster").GetComponent<RayAbility>();
        gameObject.AddComponent<Rigidbody>();
        Limit = 30;
        trans = gameObject.transform;
        pos = trans.position;
        Scal = trans.localScale;
        
    }

    void Update()
    {
        if (Flag)
        {
            if (Rays.AbilityNow != 0 && Rays.AbilityNow != 4)
            {
                trans.position = FPSCamera.transform.position + FPSCamera.transform.forward - FPSCamera.transform.right * 1.2f + FPSCamera.transform.up ;// + FPSCamera.transform.forward * 3;
                trans.rotation = FPSCamera.transform.rotation;
                trans.localScale = new Vector3(5, 5, 5);
            }
            else
            {
                trans.localScale = Scal;
                trans.position = FPSCamera.transform.position + FPSCamera.transform.right * 10 - FPSCamera.transform.up * 3 + FPSCamera.transform.forward * 5;
                trans.rotation = FPSCamera.transform.rotation;
                trans.localScale *= 0.5f;
            }
            if (Instant)
                if (5 < (time += Time.deltaTime))
                    Destroy(Instant);
        }
        else
        {
            time += (Time.deltaTime * 15);
            transform.Rotate(Vector3.up * Time.deltaTime * 30);
            if (!FallFlag)
                transform.position = pos + transform.up * Mathf.PingPong(time, Limit);
            else
                trans.position -= Vector3.up * Time.deltaTime * 50;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        FallFlag = false;
        if (col.tag == "Player")
        {
            time = 0;
            Instant = Instantiate(Resources.Load<GameObject>("MonocleMessage"));
            Destroy(gameObject.GetComponent<Rigidbody>());
            Flag = true;
            Rays.SendMessage("GetMonocle");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.transform.parent = FPSCamera.transform;
        }
        else
            pos.y = col.transform.position.y + 100;
    }
}
