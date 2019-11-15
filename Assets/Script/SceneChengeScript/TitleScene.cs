using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    void Start()
    {
        //今回は２秒後に読み込むために指定。
        Invoke("ChangeScene", 2.0f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("TiTleSelect");//シーン名が入る
        //シーンが重いのであれば、別の物を使用する
    }
}
