using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursorColtroll : MonoBehaviour
{
    //チャージ用
    public Image CircleWarp;
    public Image CircleStop;
    public Image CircleBreak;
    public Image CircleReset;
    //カーソル用
    public Image BreakAbilitycursor;
    public Image STOPAbilitycursor;
    public Image WARPAbilitycursor;
    public Image RESETAbilitycursor;
    public Image HandAbilitycursor;
    public Image Pointercursor;
    public int AbilityTrigger;
    public bool handflag;

    Image BF, SF, WF, PF, CBF, CWF, CSF, CRF, RF, HF;
    void Start()
    {
        BF  = BreakAbilitycursor.gameObject.GetComponent<Image>();
        CBF = CircleBreak.gameObject.GetComponent<Image>();
        SF  = STOPAbilitycursor.gameObject.GetComponent<Image>();
        CSF = CircleStop.gameObject.GetComponent<Image>();
        WF  = WARPAbilitycursor.gameObject.GetComponent<Image>();
        CWF = CircleWarp.gameObject.GetComponent<Image>();
        RF  = RESETAbilitycursor.gameObject.GetComponent<Image>();
        CRF = CircleReset.gameObject.GetComponent<Image>();
        PF  = Pointercursor.gameObject.GetComponent<Image>();
        HF  = HandAbilitycursor.gameObject.GetComponent<Image>();


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
                BF.enabled = false;
                CBF.enabled = false;
                SF.enabled = false;
                CSF.enabled = false;
                WF.enabled = false;
                CWF.enabled = false;
                RF.enabled = false;
                CRF.enabled = false;
                PF.enabled = true;
                break;
            case 1://warp
                BF.enabled = false;
                CBF.enabled = false;
                SF.enabled = false;
                CSF.enabled = false;
                WF.enabled = true;
                CWF.enabled = true;
                RF.enabled = false;
                CRF.enabled = false;
                PF.enabled = false;
                break;
            case 2://stop        
                BF.enabled = false;
                CBF.enabled = false;
                SF.enabled = true;
                CSF.enabled = true;
                WF.enabled = false;
                CWF.enabled = false;
                RF.enabled = false;
                CRF.enabled = false;
                PF.enabled = false;
                break;
            case 3://break
                BF.enabled = true;
                CBF.enabled = true;
                SF.enabled = false;
                CSF.enabled = false;
                WF.enabled = false;
                CWF.enabled = false;
                RF.enabled = false;
                CRF.enabled = false;
                PF.enabled = false;
                break;
            case 4://reset
                BF.enabled = false;
                CBF.enabled = false;
                SF.enabled = false;
                CSF.enabled = false;
                WF.enabled = false;
                CWF.enabled = false;
                RF.enabled = true;
                CRF.enabled = true;
                PF.enabled = false;
                break;
        }
    }
}
