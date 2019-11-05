using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMesh : MonoBehaviour
{
    private GameObject Parent;

    void Start()
    {
        Parent = transform.parent.gameObject;
        transform.gameObject.tag = Parent.tag;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Parent.SendMessage("OnChildTriggerEnter_Parent", col);
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Parent.SendMessage("OnChildTriggerExit_Parent", col);
        }
    }
    void CollStop()
    {
        Parent.SendMessage("CollStop");
    }

}
