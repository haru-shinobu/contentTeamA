using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    
    bool StageSampleCrearFlag;
    void Start()
    {
        StageSampleCrearFlag = false;
    }

    
    void Update()
    {
        if (StageSampleCrearFlag)
            SceneManager.LoadScene("Stage1");//シーン名が入る    
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") 
        {
            StageSampleCrearFlag = true;
        }
    }
}
