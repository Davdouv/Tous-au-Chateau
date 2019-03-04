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
    
    public bool onPlatform = false;
    public bool inDanger = false;



    void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Platform")
        {
            print("UNIT " + name + " IN PLATFORM AREA "+collisionInfo.gameObject.name);
            onPlatform = true;
        }
        if (!onPlatform && collisionInfo.gameObject.tag == "DangerArea" && !inDanger)
        {
            /*
             Collision system need rework, as it is not properly made : gameobject should only have one collider
             -> use empty children carrying each collider instead

             Changes shall be made with freetime : triggerzone system shall be updated
             */
            foreach(ContactPoint contactpoints in collisionInfo.contacts)
            {
                if (Vector3.SqrMagnitude(transform.position - contactpoints.point) < 1.0f)
                {
                    print("UNIT " + name + " IN DANGER AREA" + collisionInfo.gameObject.name);
                    inDanger = true;
                    return;
                }
                else
                {
                    inDanger = false;
                }
            }
            
            
            
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
    
    // Détection de gameobject servant à la redirection des villageois
    
    // Update is called once per frame
    void Update () {

        

    }
}
