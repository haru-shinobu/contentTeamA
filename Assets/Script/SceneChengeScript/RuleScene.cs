﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleScene : MonoBehaviour
{
    void a()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("TiTleSelect");//シーン名が入る 
        }
    }    
}
