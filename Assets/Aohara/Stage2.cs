using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : MonoBehaviour
{
    public void StartGame()
    {
        GameObject.Find("SoundLoop").GetComponent<AudioSource>().enabled = false;
        SceneManager.LoadScene("Stage2");//シーン名が入る
    }
}

