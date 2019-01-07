using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : TriggerZone {

    public GameObject castleDestination;
    public GameObject escapeDestination;
    public GameObject evilLord;
    public float speed;

	// Use this for initialization
	void Start () {
        //targetTag.Add("Villager");
    }

    public override void TriggerEnter(GameObject target)
    {
        Debug.Log("VICTORY !");
        float step = speed * Time.deltaTime;

        // villagers walk inside the castle
        target.transform.position = Vector3.MoveTowards(target.transform.position, castleDestination.transform.position, step);
        // evil lord escapes
        evilLord.transform.position = Vector3.MoveTowards(target.transform.position, escapeDestination.transform.position, step);
    }
}
