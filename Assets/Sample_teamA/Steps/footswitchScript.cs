using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footswitchScript : MonoBehaviour
{
    public bool SwitchOrPerception;
    public int PerceptionTime;
    public GameObject Target;
    public GameObject Target2;
    

    MeshRenderer mesh;
    void Start()
    {
        mesh = gameObject.transform.GetComponent<MeshRenderer>();
        mesh.material.DisableKeyword("_EMISSION");
        if (Target.transform.GetComponent<Doormove>())
        {
            if (++PerceptionTime <= 90)
                PerceptionTime *= 60;
            Target.transform.GetComponent<Doormove>().PerceptionTime = PerceptionTime;
        }
        if (SwitchOrPerception)
        {
            mesh.enabled = false;
            SwitchOrPerception = true;
        }else
            SwitchOrPerception = false;
    }
    void OnTriggerEnter(Collider col)
    {
        
        mesh.material.EnableKeyword("_EMISSION");

        if (SwitchOrPerception)
        {
            Target.SendMessage("PerceptionChange");
            if (Target2)
                Target2.SendMessage("PerceptionChange");
        }
        else
        {
            Target.SendMessage("SwichChange");
            if (Target2)
                Target2.SendMessage("SwichChange");
        }
    }
    void OnTriggerExit(Collider col)
    {
        mesh.material.DisableKeyword("_EMISSION");
    }
}
//SwitchOrPerception
