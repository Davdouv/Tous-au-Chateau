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
	

	// Use this for initialization
	void Start () {
		transform.LookAt(GameObject.Find("Objectif").transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		// movement
		transform.position += speed * transform.forward * Time.deltaTime;
		
			
	}
			
}
