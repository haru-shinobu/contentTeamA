using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Scene : MonoBehaviour
{
    public bool Stage2CrearFlag;

    void State()
    {
        Stage2CrearFlag = false;
    }
    // a is called once per frame
    void a()
    {
        if (Stage2CrearFlag)
        {
            SceneManager.LoadScene("Stage3");//シーン名が入る 
        }
    }
}
