using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
Boid Collision
Détecte les gameobjects sur le chemin directe du gameobject auquel il est accroché.
Il tourne s'il rencontre un gameobject catégorisé "Sign".
Il appelle la méthode BoidStatus.getDamaged() lorsqu'il entre en collision avec un certain objet à définir.
Il appelle la méthode BoidMovement.Turn() lorsqu'il rencontre un objet "Sign".

Nécessite :
	- un component collider
	- un component rigidbody (collision en mode continue)
	- un autre gameobject à collide qui possède un collider
	- un component BoidStatus
    - un component BoidMovement
    - l'existence de tag
*/

public class BoidCollision : MonoBehaviour {

	private RaycastHit hitInfo;
	private bool touched= false;
    private bool onPlatform = false;


    void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.tag == "Platform")
        {
            onPlatform = true;
        }
        if (!onPlatform && collisionInfo.tag == "DangerArea")
        {
            print("UNIT " + name + " IN DANGER AREA");
            
        }
    }
    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Platform")
        {
            onPlatform = false;
        }
        if (collisionInfo.tag == "DangerArea")
        {
            print("UNIT " + name + " LEAVING DANGER AREA");
        }
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        if(!onPlatform && collisionInfo.gameObject.tag == "DangerArea")
        	gameObject.GetComponent<BoidStatus>().getDamaged();
    }
    // Détection de gameobject servant à la redirection des villageois
    void CheckforSign()
    {
        // Résultat de Raycast
        touched = Physics.Raycast(
            transform.position,
            transform.forward,
            out hitInfo,
            3.0f
            );
        if (touched && hitInfo.collider.tag == "Sign")
        {
            GetComponent<BoidMovement>().Turn(hitInfo.collider);
        }
    }
    // Update is called once per frame
    void Update () {

        CheckforSign();

        
    }
}
