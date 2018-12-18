using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkTest : MonoBehaviour {

	public Transform t;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = t.position;
	}

	// Update is called once per frame
	void Update () {

	}
}
