using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cleartextScript : MonoBehaviour
{
    Text word;
    float timer;
    int count;
    
    void Start()
    {
        RectTransform rt = gameObject.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(600, 300);
        timer = 0;
        word = transform.GetComponent<Text>();

        word.text = "出口だ！";
        word.fontSize = 64;
        word.resizeTextForBestFit = true;
        word.color = new Color(0, 0, 0, 255);
        count = 0;
    }

    
    void a()
    {
        if (timer < 3)
            timer += Time.deltaTime;
        else
        {
            count++;

            switch ((int)count/10) {
                case 0:
                    word.text = "S    " + "\n" + "     ";
                    break;
                case 1:
                    word.text = "ST   " + "\n" + "     ";
                    break;
                case 2:
                    word.text = "STA  " + "\n" + "     ";
                    break;
                case 3:
                    word.text = "STAG " + "\n" + "     ";
                    break;
                case 4:
                    word.text = "STAGE" + "\n" + "      ";
                    break;
                case 5:
                    word.text = "STAGE" + "\n" + "C     ";
                    break;
                case 6:
                    word.text = "STAGE" + "\n" + "CL    ";
                    break;
                case 7:
                    word.text = "STAGE" + "\n" + "CLE   ";
                    break;
                case 8:
                    word.text = "STAGE" + "\n" + "CLEA  ";
                    break;
                case 9:
                    word.text = "STAGE" + "\n" + "CLEAR ";
                    break;
                case 10:
                    word.text = "STAGE" + "\n" + "CLEAR!";
                    break;
            }
        }
    }
}
