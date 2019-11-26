using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameoverScript : MonoBehaviour
{

    RectTransform rec;
    Vector3 scale;
    float com;
    void Start()
    {
        com = 0;
        rec = gameObject.GetComponent<RectTransform>();        
    }

    // Update is called once per frame
    void Update()
    {
        scale = new Vector3(1, com-0.2f, 1);
        rec.localScale = scale;
        if (com < 3)
            com += Time.deltaTime*0.2f;
    }
}
