using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamWayScript : MonoBehaviour
{
 //   private void OnBecameVisible()    {    }
    private void OnBecameInvisible()
    {
        foreach (Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.SetActive(false);
        }
        enabled = false;
    }

    void OnWillRenderObject()
    {
#if UNITY_EDITOR
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
#endif
        {
            enabled = true;
            foreach (Transform childTransform in gameObject.transform)
            {
                childTransform.gameObject.SetActive(false);
            }
        }
    }
}
