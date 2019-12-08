using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshEyeIcon : MonoBehaviour
{
    Renderer spriteRenderer;
    MeshRenderer mesh;
    public Material sprite1;
    public Material sprite2;
    public Material sprite3;
    GameObject undereye;
    private Vector3 nor;
    private Vector3 pon;
    float timer;
    
    bool Flag;
    void Start()
    {
        Destroy(gameObject.GetComponent<MeshCollider>());
        spriteRenderer = GetComponent<Renderer>();
        spriteRenderer.material = sprite1;
        undereye = Instantiate(gameObject, transform.position - new Vector3(0, 25, 0), transform.rotation);
        Destroy(undereye.GetComponent<meshEyeIcon>());
        undereye.gameObject.GetComponent<Renderer>().sharedMaterial = sprite2;
        Destroy(undereye.gameObject.GetComponent<MeshCollider>()); 
        gameObject.transform.GetChild(0).GetComponent<Light>().enabled = false;
        Flag = false;
        mesh = gameObject.GetComponent<MeshRenderer>();
        timer = 0;
    }

    public void Stopenter(Vector3 normal,Vector3 point)
    {
        nor = normal;
        pon = point;
    }

    void Update()
    {
        if (!Flag)
        {
            transform.position = Vector3.Lerp(transform.position, pon, Time.deltaTime);
            if (transform.position.y - pon.y < 1)
            {
                gameObject.transform.GetChild(0).GetComponent<Light>().enabled = true;
                gameObject.transform.GetChild(0).transform.position = pon;
                gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
                Flag = !Flag;
            }
        }
        else
        {
            timer += Time.deltaTime;
            gameObject.transform.GetComponent<MeshRenderer>().enabled = true;
            mesh.material.color = new Color32(19, 255, 0, (byte)(1/3 * timer));
            Destroy(gameObject.transform.GetChild(0).GetComponent<Light>());
            if (timer >= 3)
            {
                stopend();
            }
        }
    }

    void stopend()
    {
        Destroy(undereye);
        Destroy(gameObject);
    }
}
