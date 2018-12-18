using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
Boid Status
Définit les HP et la Motivation du gameobject.
Il est détruit si ses HP tombent à 0.
Nécessite :
	- un autre component pour appeller getDamaged()
	
*/

public class BoidStatus : MonoBehaviour {

	public float life_point, motivation_point;
	// Use this for initialization
	void Start () {
		life_point = 1;
		motivation_point = 100;
	}
	
	public void getDamaged(){
		life_point-=1*Time.deltaTime;
	}
	// Update is called once per frame
	void Update () {
        if (life_point < 0)
        {
            GetComponent<DyingVillager>().isAlive = false;
            GetComponent<BoidMovement>().speed = 0.0f;
            GetComponent<NavMeshAgent>().enabled = false;
        }
            
            
	}
}
