using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialChange : MonoBehaviour
{

    public Material transparent_mat;
    public Material transparentRed_mat;
    Renderer rend;
    public bool inCollision;

    void Start()
    {
        rend = GetComponent<Renderer>();
        inCollision = false;
    }

    void Update()
    {
        if (inCollision)
        {
            //rend.material = transparentRed_mat;
            ChangeMaterial(transparentRed_mat);
        }
        else
        {
            //rend.material = transparent_mat;
            ChangeMaterial(transparent_mat);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if () {  add distance check
            inCollision = true;
        /*}
        else
        {
            InCollision = false;
        }*/
    }
    private void OnTriggerExit(Collider other)
    {
        //if () {  add distance check
        inCollision = false;
        /*}
        else
        {
            InCollision = false;
        }*/
    }

    void ChangeMaterial(Material newMat)
    {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (int i = 0; i < rend.materials.Length; ++i)
            {
                mats[i] = newMat;
            }
            rend.materials = mats;
        }
    }
}