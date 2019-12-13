﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    AudioSource sound;
    public AudioClip SentakuSE;
    bool Flag;
    float timer;
    void Awake()
    {
        Flag = false;
        timer = 0;
        SentakuSE = (AudioClip)Resources.Load("sentaku-SE");
        sound = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Flag)
            timer += Time.deltaTime;
        if (timer > 0.5)
            SceneManager.LoadScene("GameRule");
    }


    public void StartGame()
    {
        sound.PlayOneShot(SentakuSE);
        Flag = true;
    }
}
