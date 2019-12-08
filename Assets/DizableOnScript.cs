using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizableOnScript : MonoBehaviour
{
    [SerializeField]
    Material mat;
    protected float clossover;
    private bool Flag;
    public int BurnDelay = 2;
    void Start()
    {
        tag = "Switch";
        Flag = false;
        clossover = 0.8f;
        mat.SetFloat("_Progress", clossover);
    }

    // a is called once per frame
    void TriggerOn()
    {
        Flag = true;
    }

    void a()
    {
        if (Flag)
        {
            clossover -= Time.deltaTime * 0.1f;
            if (clossover <= 0)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.transform.parent.GetComponent<Canvas>().enabled = false;
                clossover = 0.8f;
                Flag = false;
            }
            mat.SetFloat("_Progress", clossover);
        }
    }
    
}
