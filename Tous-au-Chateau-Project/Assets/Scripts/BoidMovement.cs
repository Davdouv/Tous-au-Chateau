using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
Boid Movement
Déplace le gameobject en ligne droite.
Nécessite :
	- un gameobject nommé "Objectif" comme direction initiale
    - un Rigidbody:
        kinematic = true,
        gravity = false (?), 
        interpolate = none, 
        collision detection = continuous
    - Une surface avec un component Navmesh Surface sur lequel se déplacer
    - un component Navmesh Agent
autre : des obstacles avec Navmesh Obstacle


*/
public class BoidMovement : MonoBehaviour
{

    public float speed = 1.50f;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(GameObject.Find("Objectif").transform.position);
        rb.freezeRotation = true;
        rb.isKinematic = true;
    }

    public void Turn(Collider direction)
    {
        if (direction.name == "LeftWoodSign")
        { 
            transform.Rotate(0, 90, 0);
        }else{
            transform.Rotate(0, -90, 0);
        }
    }

    // FixedUpdate est conseillé en cas d'instructions sur la physique
    void FixedUpdate()
    {
        // movement
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);

        //transform.position += speed * transform.forward * Time.deltaTime;
        //transform.Translate(transform.forward * Time.deltaTime);
        //rb.AddForce(transform.forward * Time.fixedDeltaTime, ForceMode.Force);
        //rb.position = transform.position + transform.forward * speed * Time.fixedDeltaTime;
        //print(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        //rb.velocity = transform.forward * speed * Time.fixedDeltaTime;
    }

}
