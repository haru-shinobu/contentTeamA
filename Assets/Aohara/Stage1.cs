using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    public void StartGame()
    {
        GameObject.Find("SoundLoop").GetComponent<AudioSource>().enabled = false;
        SceneManager.LoadScene("Stage1");//シーン名が入る
    }
}

