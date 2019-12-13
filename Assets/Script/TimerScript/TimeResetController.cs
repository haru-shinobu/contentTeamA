using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeResetController : MonoBehaviour
{

    void Start()
    {
        //カーソル表示・カーソルを画面内にロック
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
