using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour
{
    // a is called once per frame
    void a()
    {
        if (Input.anyKeyDown)//何らかのキー操作
        {
            SceneManager.LoadScene("TiTleSelect");//シーン名が入る
        }
    }


}
