﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursorColtroll : MonoBehaviour
{
    //チャージ用
    public Image CircleWarp;
    public Image CircleStop;
    public Image CircleBreak;
    //カーソル用
    public Image BreakAbilitycursor;
    public Image STOPAbilitycursor;
    public Image WARPAbilitycursor;
    public Image HandAbilitycursor;
    public Image Pointercursor;
    public int AbilityTrigger;
    public bool handflag;
    void Start()
    {
        handflag = false;
    }
    void Update()
    {
        if (handflag)
            HandAbilitycursor.gameObject.GetComponent<Image>().enabled = true;
        else
            HandAbilitycursor.gameObject.GetComponent<Image>().enabled = false;

        AbilityTrigger = this.gameObject.GetComponent<RayAbility>().AbilityNow;
        switch (AbilityTrigger) {
            case 0://normal
                BreakAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                STOPAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                WARPAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                Pointercursor.gameObject.GetComponent<Image>().enabled = true;
                CircleWarp.gameObject.GetComponent<Image>().enabled = false;
                CircleStop.gameObject.GetComponent<Image>().enabled = false;
                CircleBreak.gameObject.GetComponent<Image>().enabled = false;
                break;
            case 1://warp
                BreakAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                STOPAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                WARPAbilitycursor.gameObject.GetComponent<Image>().enabled = true;
                Pointercursor.gameObject.GetComponent<Image>().enabled = false;
                CircleWarp.gameObject.GetComponent<Image>().enabled = true;
                CircleStop.gameObject.GetComponent<Image>().enabled = false;
                CircleBreak.gameObject.GetComponent<Image>().enabled = false;
                break;
            case 2://stop
                BreakAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                STOPAbilitycursor.gameObject.GetComponent<Image>().enabled = true;
                WARPAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                Pointercursor.gameObject.GetComponent<Image>().enabled = false;
                CircleWarp.gameObject.GetComponent<Image>().enabled = false;
                CircleStop.gameObject.GetComponent<Image>().enabled = true;
                CircleBreak.gameObject.GetComponent<Image>().enabled = false;
                break;
            case 3://break
                BreakAbilitycursor.gameObject.GetComponent<Image>().enabled = true;
                STOPAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                WARPAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                Pointercursor.gameObject.GetComponent<Image>().enabled = false;
                CircleWarp.gameObject.GetComponent<Image>().enabled = false;
                CircleStop.gameObject.GetComponent<Image>().enabled = false;
                CircleBreak.gameObject.GetComponent<Image>().enabled = true;
                break;
            case 4://reset
                BreakAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                STOPAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                WARPAbilitycursor.gameObject.GetComponent<Image>().enabled = false;
                Pointercursor.gameObject.GetComponent<Image>().enabled = false;
                CircleWarp.gameObject.GetComponent<Image>().enabled = false;
                CircleStop.gameObject.GetComponent<Image>().enabled = false;
                CircleBreak.gameObject.GetComponent<Image>().enabled = false;
                break;
        }
    }
}