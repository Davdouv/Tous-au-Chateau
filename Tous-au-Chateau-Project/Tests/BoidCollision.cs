using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Boid Collision
Détecte les gameobjects sur le chemin directe du gameobject auquel il est accroché.
Il tourne s'il rencontre un gameobject nommé "panneau".
Il appelle la méthode BoidStatus.getDamaged() lorsqu'il entre en collision avec un certain objet à définir.
Nécessite :
	- un component collider
	- un component rigidbody (avec continuous pour collision detection)
	- un autre gameobject à collide qui possède un collider
	- un component BoidStatus
*/

public class BoidCollision : MonoBehaviour {

	private RaycastHit hitInfo;
	private bool touched= false;

	

	void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.name == "Wall")
        	gameObject.GetComponent<BoidStatus>().getDamaged();
    }
	// Update is called once per frame
	void Update () {
		
		// detection d'objet
		touched = Physics.Raycast(
			transform.position, 
			transform.forward, 
			out hitInfo, 
			3.0f
			);
		if(touched){
			string object_name = hitInfo.transform.gameObject.name;
			print("object hit : "+object_name);
			if(object_name == "panneau")
				transform.Rotate(0, 90, 0);
		}
	}
}
