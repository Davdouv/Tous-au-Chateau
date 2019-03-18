using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialChange : MonoBehaviour
{

    public Material transparent_mat;
    public Material transparentRed_mat;
    Renderer rend;
    public bool InCollision;

    void Start()
    {
        rend = GetComponent<Renderer>();
        InCollision = false;
    }

    void Update()
    {
        if (InCollision)
        {
            rend.material = transparentRed_mat;
        }
        else
        {
            rend.material = transparent_mat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if () {  add distance check
            InCollision = true;
        /*}
        else
        {
            InCollision = false;
        }*/
    }
    private void OnTriggerExit(Collider other)
    {
        //if () {  add distance check
        InCollision = false;
        /*}
        else
        {
            InCollision = false;
        }*/
    }
}