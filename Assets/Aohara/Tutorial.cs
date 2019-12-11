using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("GameRule");//シーン名が入る
    }
}

