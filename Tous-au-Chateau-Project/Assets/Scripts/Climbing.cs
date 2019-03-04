using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* When using Climbing, make sure to verifiy these following conditions:
 * - Climbable is not floating
 * - terrain and nonclimbable-obstacle are correctly tagged as Ground and Obstacle
*/
 
public class Climbing : MonoBehaviour {

    RaycastHit hit;
    public float stepHeight;

    // Use this for initialization
    void Start () {
        print("lossyScale : " + transform.lossyScale);
        print("Ray feet " + (transform.lossyScale.z / 2.0f + 0.1f) );
        print("Ray head " + (transform.lossyScale.z / 2.0f + 0.1f) + " from " + (transform.position + new Vector3(0, transform.localScale.y / 2.0f, 0)) );
        print("Ray down " + (transform.localScale.y - stepHeight + 0.1f) + " from " + (transform.position + new Vector3(0, transform.localScale.y / 2.0f, transform.lossyScale.z / 2.0f + 0.1f) ));
    }

    // Update is called once per frame
    void Update () {
        //Raycasting
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, transform.lossyScale.z/2.0f + 0.1f))
        {
            print("First contact at : " + hit.point);
            if (!Physics.Raycast(transform.position + new Vector3(0,stepHeight +0.1f,0), transform.forward, out hit, transform.lossyScale.z / 2.0f + 0.1f))
            {
                
                if (Physics.Raycast(transform.position + new Vector3(0, stepHeight + 0.1f, transform.lossyScale.z / 2.0f + 0.1f), -transform.up, out hit, stepHeight+0.1f))
                {
                    print("Hauteur hit-pos : "+ (hit.point.y - transform.position.y));
                    if (hit.transform.tag == "Ground" || hit.transform.tag == "Obstacle") return;
                    if(hit.point.y -  transform.position.y <  stepHeight)
                    {
                        Warp(hit.point);
                    }

                }

            }
            else
            {
                print("hit") ;
            }

        }
    }

    private void Warp(Vector3 point)
    {
        transform.position = point;
    }
}
