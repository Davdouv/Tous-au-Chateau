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

    private void OnCollisionEnter(Collision collision)
    {
        
        int layerid = LayerMask.NameToLayer("Building");
        if (collision.gameObject.layer != layerid) return;
        if (Physics.Raycast(new Ray(transform.position + new Vector3(0, 0.01f, 0), transform.forward), out hit, 2f))
        {
            Debug.DrawRay(transform.position + new Vector3(0,0.01f,0) , transform.forward,Color.black,10f);
            print("First contact at : " + hit.point);
            if (!Physics.Raycast(transform.position + new Vector3(0, stepHeight + 0.1f, 0), transform.forward, out hit, transform.lossyScale.z / 2.0f + 0.1f))
            {
                // 1<<int est une opération qui convertit int en bits pour l'équivalent en bits du layer
                if (Physics.Raycast(new Ray(transform.position + new Vector3(0, stepHeight + 0.1f, transform.lossyScale.z / 2.0f + 0.1f), -transform.up), out hit, stepHeight + 0.1f,1<<layerid)) 
                {
                    Debug.DrawRay(transform.position + new Vector3(0, stepHeight + 0.1f, transform.lossyScale.z / 2.0f + 0.1f), -transform.up, Color.black, 10f);
                    print(hit.collider.name);
                    Warp(hit.point);
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward, Color.black, 10f);

            print("nothing in front of me "+ layerid + " " + collision.gameObject.name);
        }
    }

    private void Warp(Vector3 point)
    {
        transform.position = point;
    }
    private void Update()
    {
        Debug.DrawLine(transform.position + new Vector3(0,0.5f,0), transform.position+ transform.forward + new Vector3(0, 0.5f, 0), Color.black);
    }
}
