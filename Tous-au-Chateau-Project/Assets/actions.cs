using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actions : MonoBehaviour {

    VRTK.VRTK_ControllerEvents events;

    bool trigger;
    Vector3 currentPos;

    // Use this for initialization
    void Start () {
        events = GetComponent<VRTK.VRTK_ControllerEvents>();
	}
	
	// Update is called once per frame
	void Update () {
        if (events.triggerPressed)
        {
            trigger = true;
            currentPos = gameObject.transform.position;
        }
        else
        {
            trigger = false;
        }

        if (trigger)
        {
            currentPos = gameObject.transform.position;
            Debug.Log(currentPos);
        }
	}

    //void OnTrigg
}
