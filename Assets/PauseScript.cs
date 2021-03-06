﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    AudioSource sound;
    AudioClip SentakuSE;

    GameObject Icon1;
    GameObject Icon2;
    GameObject Panel2;
    GameObject Scroll1;
    GameObject Scroll2;
    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        sound = gameObject.GetComponent<AudioSource>();
        SentakuSE = (AudioClip)Resources.Load("sentaku-SE");
    }
    void Start()
    {
        sound.PlayOneShot(SentakuSE);
        Icon1 = gameObject.transform.GetChild(1).gameObject;
        Icon2 = gameObject.transform.GetChild(3).gameObject;
        Panel2 = gameObject.transform.GetChild(2).gameObject;
        Scroll1 = gameObject.transform.GetChild(2).GetChild(0).gameObject;
        Scroll2 = gameObject.transform.GetChild(2).GetChild(1).gameObject;
        Panel2.SetActive(false);
        Scroll1.SetActive(false);
        Scroll2.SetActive(false);

    }
    public void OnClickBlackIcon()
    {
        sound.PlayOneShot(SentakuSE);
        Icon1.SetActive(false);
        Icon2.SetActive(false);
        Panel2.SetActive(true);
        Scroll1.SetActive(true);
        Scroll2.SetActive(false);
    }

    public void OnClickControllImage()
    {
        sound.PlayOneShot(SentakuSE);
        Icon1.SetActive(false);
        Icon2.SetActive(false);
        Panel2.SetActive(true);
        Scroll1.SetActive(false);
        Scroll2.SetActive(true);
    }
    public void OnClickControllImage2()
    {
        sound.PlayOneShot(SentakuSE);
        Icon1.SetActive(true);
        Icon2.SetActive(true);
        Scroll1.SetActive(false);
        Scroll2.SetActive(false);
        Panel2.SetActive(false);
    }

    public void OnClickResetIcon()
    {
        sound.PlayOneShot(SentakuSE);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("GameMaster").GetComponent<GameStageSetting>().GAMEOVER();
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
}

