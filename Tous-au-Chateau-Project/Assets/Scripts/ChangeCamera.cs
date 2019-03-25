using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera[] cameras;

    // Use this for initialization
    void Start()
    {
        cameras[0].enabled = true;
        cameras[1].enabled = false;
        cameras[2].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cameras[0].enabled = false;
            cameras[1].enabled = true;
            cameras[2].enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            cameras[0].enabled = false;
            cameras[1].enabled = false;
            cameras[2].enabled = true;
        }
    }
}