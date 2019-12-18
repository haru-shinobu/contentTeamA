using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollTexScript : MonoBehaviour
{
    public GameObject WarldSpaceText;
    AudioSource audioSource;
    public AudioClip scrollSE;
    GameObject Player;
    Vector3 pos, scaler;
    Quaternion qua;
    bool moveFlag;
    float mtime;
    BoxCollider box;
    bool Flag;
    Canvas canvas;
    void Start()
    {
        moveFlag = false;
        scrollSE = (AudioClip)Resources.Load("scroll-SE");
        audioSource = gameObject.GetComponent<AudioSource>();
        tag = "Switch";
        Player = GameObject.FindWithTag("Player");
        pos = WarldSpaceText.transform.position;
        qua = WarldSpaceText.transform.localRotation;
        scaler = WarldSpaceText.transform.localScale;

        canvas = WarldSpaceText.GetComponent<Canvas>();
        canvas.enabled = false;
        Flag = false;
        if (WarldSpaceText.transform.GetChild(0).GetComponent<BoxCollider>())
        {
            Flag = true;
            box = WarldSpaceText.transform.GetChild(0).GetComponent<BoxCollider>();
            box.enabled = false;
        }
    }
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (moveFlag)
        {
            mtime = Time.deltaTime * 0.1f;
            
            WarldSpaceText.transform.position = Vector3.Lerp(WarldSpaceText.transform.position, pos, mtime);
            WarldSpaceText.transform.localScale = Vector3.Lerp(WarldSpaceText.transform.localScale, scaler, mtime);
            WarldSpaceText.transform.localRotation = Quaternion.Lerp(WarldSpaceText.transform.localRotation, qua, mtime);
            if (mtime >= 1)
            {
                moveFlag = false;
                mtime = 0;
            }
        }
    }
    void TriggerOn()
    {
        audioSource.PlayOneShot(scrollSE);
        canvas.enabled = true;
        WarldSpaceText.transform.position = gameObject.transform.position;
        WarldSpaceText.transform.localScale = new Vector3(0, 0, 0);
        WarldSpaceText.transform.rotation = Player.transform.localRotation;
        moveFlag = true;
        if (Flag)
        {
            box.enabled = true;
        }
    }
}
