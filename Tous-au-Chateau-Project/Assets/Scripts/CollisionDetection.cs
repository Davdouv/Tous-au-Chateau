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

public class CollisionDetection : MonoBehaviour {

	public RaycastHit hitInfo;
	private bool touched= false;
    public bool onPlatform = false;
    public bool inDanger = false;



    void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.tag == "Platform")
        {
            print("UNIT " + name + " IN PLATFORM AREA "+collisionInfo.name);
            onPlatform = true;
        }
        if (!onPlatform && collisionInfo.tag == "DangerArea")
        {
            print("UNIT " + name + " IN DANGER AREA" + collisionInfo.name);
            inDanger = true;
            
        }
    }
    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Platform")
        {
            print("UNIT " + name + " LEAVING PLATFORM AREA" + collisionInfo.name);
            onPlatform = false;
        }
        if (collisionInfo.tag == "DangerArea")
        {
            print("UNIT " + name + " LEAVING DANGER AREA" + collisionInfo.name);
            inDanger = false;
        }
    }
    /*
    void OnCollisionStay(Collision collisionInfo)
    {
        print("OncollisionStay de " + name + " avec "+collisionInfo.gameObject.name);
        if(!onPlatform && collisionInfo.gameObject.tag == "DangerArea")
        {
            print("UNIT " + name + " TAKES DAMAGE" + collisionInfo.gameObject.name);
            GetComponent<BoidStatus>().getDamaged();
        }
            
    }
    */
    // Détection de gameobject servant à la redirection des villageois
    public bool CheckforSign()
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
            return true;
            
        }
        return false;
    }
    // Update is called once per frame
    void Update () {

        

    }
}
