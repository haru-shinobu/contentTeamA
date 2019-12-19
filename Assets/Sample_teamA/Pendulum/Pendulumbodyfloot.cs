using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulumbodyfloot : MonoBehaviour
{
    bool Flag;
    void Start()
    {
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        Flag = false;
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.SetParent(transform);
            Flag = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.SetParent(null);
            col.transform.localScale = new Vector3(50, 150, 50);
            Flag = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        transform.position = transform.root.GetChild(0).GetChild(2).gameObject.transform.position;
        if (Flag)
            transform.GetChild(0).gameObject.transform.rotation = transform.rotation;
    }
    void CollStop()
    {
        transform.root.GetChild(0).gameObject.GetComponent<PendulumShaft>().StopActivity();
    }
}
