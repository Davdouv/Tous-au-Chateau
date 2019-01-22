using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actions : MonoBehaviour {

    public ResourceManager resourceM;

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (trigger)
        {
            if (other.tag == "Wood")
            {
                //other.crush()   <= for the next sprint
                Debug.Log("INTO WOOD");
                resourceM.AddWood(10);
                Destroy(other.gameObject);
            }
            else if (other.tag == "Stone")
            {
                Debug.Log("INTO STONE");
                resourceM.AddStone(10);
                Destroy(other.gameObject);
            }
            else if (other.tag == "Food")
            {
                Debug.Log("INTO FOOD");
                resourceM.AddFood(10);
                Destroy(other.gameObject);
            }
        }
        
    }

    //void OnTrigg
}
