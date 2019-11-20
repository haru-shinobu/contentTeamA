using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayTrigger : MonoBehaviour
{
    public bool call_enter;

    public GameObject Parent;
    
    void Start()
    {
        Parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (call_enter)
        {
            Parent.SendMessage("OnChildTriggerEnter", col);
        }
    }
    void CollStop()
    {
        Parent.SendMessage("CollStop");
    }
}
