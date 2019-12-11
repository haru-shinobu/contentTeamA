using UnityEngine;
using System.Collections;

public class SoundLoop : MonoBehaviour
{
    public bool DontDestroyEnabled = true;
    static bool SoundFlag = true;
    // Use this for initialization
    void Awake()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
        if (SoundFlag)
            SoundFlag = false;
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (DontDestroyEnabled)
        {
            // Sceneを遷移してもオブジェクトが消えないようにする
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.A) == true)
//        {
// //           Debug.Log("Now scene is " + Application.loadedLevelName);
//            // Aキーを押すとシーンが遷移する
//            Application.LoadLevel("StageSellect");
//        }
//    }
}