using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearTimeManager : MonoBehaviour
{
    int ScoreTime;
    private int bestScore; // ベストタイムの値を格納する変数
    public Text finishTime;  // FinishTimeテキストの変数
    public Text bestTime; // BestTimeテキストの変数
    public GameObject finishUI; // Finishオブジェクトの変数
    public Image Panel;                //透明度を変更するパネルのイメージ
    

    float fadeSpeed = 0.01f;        //透明度が変わるスピードを管理
    
    float red, blue, green, alfa;   //パネルの色、不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
    

    // Start is called before the first frame update
    void Start()
    {
        ScoreTime = 0;
        int t1 = Stage1TimeManager.GetTime();
        int t2 = Stage2TimeManager.GetTime();
        int t3 = Stage3TimeManager.GetTime();
        if(t1 > 0)
        {
            ScoreTime = t1;
        }
        else if (t2 > 0)
        {
            ScoreTime = t2;
        }
        else if (t3 > 0)
        {
            ScoreTime = t3;
        }

        if (PlayerPrefs.HasKey("ScoreTime"))
        {
            

            bestScore = PlayerPrefs.GetInt("ScoreTime");

        }
        else
        {
            bestScore = 999;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;
        }
        if (isFadeOut == false)
        {
            finishUI.SetActive(true);

            // クリア時間を整数にしてscoreに格納
            int score = ScoreTime;

            // テキストの値を変える
            finishTime.text = "FinishTime = " + score;
            bestTime.text = "ScoreTime = " + bestScore;

            // もしクリアスコアがベストスコアより小さい場合
            if (bestScore > score)
            {
                // ベストスコアにクリアスコアを入れる
                PlayerPrefs.SetInt("BestScore", score);
            }
        }
        if (Input.anyKey)
        {
            alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
            SetAlpha();                      //b)変更した不透明度パネルに反映する
            if (alfa <= 0)
            {                    //c)完全に透明になったら処理を抜ける
                isFadeIn = false;
            }
            if (isFadeIn == false)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
    
    void SetAlpha()
    {
        Panel.color = new Color(red, green, blue, alfa);
    }

    void ResultTime()
    {
        Debug.Log(ScoreTime);
    }

    public void OnRetry()
    {
        alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
        }
        if (isFadeIn == false)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
