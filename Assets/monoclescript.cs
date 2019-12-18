
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
    void Start()
    {
        Flag = false;
        FPSCamera = GameObject.Find("FPSCamera");
        Rays = GameObject.Find("GameMaster").GetComponent<RayAbility>();
        Limit = 30;
        pos = gameObject.transform.position;
    }

    void Update()
    {
        if (Flag)
        {//取得時
            if (Rays.AbilityNow != 0 && Rays.AbilityNow != 4)
            {
                gameObject.transform.position = FPSCamera.transform.position + FPSCamera.transform.right * -4 + FPSCamera.transform.up * 3 + FPSCamera.transform.forward * 3;
                gameObject.transform.rotation = FPSCamera.transform.rotation;
                if (!Rays.LostMonocle())
                    Destroy(gameObject);
            }
            else
            {
                gameObject.transform.position = FPSCamera.transform.position + FPSCamera.transform.right * 15 - FPSCamera.transform.up * 5 + FPSCamera.transform.forward * 5;
                gameObject.transform.rotation = FPSCamera.transform.rotation;
            }
        }
        else
        {//取得してない時
            time += (Time.deltaTime * 15);
            transform.Rotate(Vector3.up * Time.deltaTime*30);
            transform.position = pos + transform.up * Mathf.PingPong(time, Limit);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (!Rays.MonocleCheck())
        {
            if (col.tag == "Player")
            {
                Flag = true;
                Rays.SendMessage("GetMonocle");
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.transform.parent = FPSCamera.transform;
            }
        }
    }
}
