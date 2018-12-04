using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour {

	/*
	*	Detects if a game object with the Tag "Villagers" crosses the box collider
	* on which this script is linked
	* Attention! Collider object "col" must have rigidBody component
	*/
	void OnTriggerEnter (Collider col) {
		Debug.Log("Something is inside!");
		if (col.gameObject.CompareTag("Villager")) {
			Debug.Log("Villagers are inside!");
		}
	}

	void OnTriggerStay (Collider col) {
		Debug.Log("Still insied!");
		if (col.gameObject.CompareTag("Villager")) {
			Debug.Log("Villager is still inside!");
		}
	}
}
