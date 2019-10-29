using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandRotetion : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("HandImage");
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(15, 100);
    }
}
