using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Boid Movement
Déplace le gameobject en ligne droite.
Nécessite :
	- un gameobject nommé "Objectif" comme direction initiale

*/
public class BoidMovement : MonoBehaviour {

	public float speed = 1.50f;
    Vector3 face;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(GameObject.Find("Objectif").transform.position);
        face = transform.forward.normalized;
        rb.freezeRotation = true;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // movement
        //transform.position += speed * transform.forward * Time.deltaTime;
        //transform.Translate(transform.forward * Time.deltaTime);
        //rb.MovePosition(transform.position + transform.forward  * speed * Time.fixedDeltaTime);
        //rb.AddForce(transform.forward*100 * Time.fixedDeltaTime, ForceMode.Force);
        rb.position = transform.position + transform.forward * speed * Time.fixedDeltaTime;
        print(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        //rb.velocity = transform.forward * speed * Time.fixedDeltaTime;
    }
			
}
