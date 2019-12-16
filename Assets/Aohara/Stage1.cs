using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
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
        {
            GameObject.Find("SoundLoop").GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene("Stage1");
        }
    }


    public void StartGame()
    {
        sound.PlayOneShot(SentakuSE);
        Flag = true;
    }
}
