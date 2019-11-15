using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footswitchScript : MonoBehaviour
{
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }
    void OnTriggerEnter(Collider col)
    {
        gameObject.transform.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        Target.SendMessage("SwichChange");
    }
    void OnTriggerExit(Collider col)
    {
        gameObject.transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }
}
