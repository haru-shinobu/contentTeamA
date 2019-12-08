using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSelectScene : MonoBehaviour
{
    // a is called once per frame
    void a()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("StageSelect");//シーン名が入る
            //シーンが重いのであれば、別の物を使用する
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManager.LoadScene("GameRule");//シーン名が入る
            //シーンが重いのであれば、別の物を使用する
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManager.LoadScene("GameCredit");//シーン名が入る
            //シーンが重いのであれば、別の物を使用する
        }
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Title");
    }
}
