using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : TriggerZone {

	// Use this for initialization
	void Start () {
        //targetTag.Add("Villager");
    }

    public override void TriggerEnter(Collider other)
    {
        Debug.Log("VICTORY !");
    }
}
