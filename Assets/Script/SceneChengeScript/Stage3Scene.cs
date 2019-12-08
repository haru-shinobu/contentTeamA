using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Scene : MonoBehaviour
{
    public bool Stage3CrearFlag;

    void State()
    {
        Stage3CrearFlag = false;
    }
    // a is called once per frame
    void a()
    {
        if (Stage3CrearFlag)
        {
            SceneManager.LoadScene("Result");//シーン名が入る 
        }
    }
}
