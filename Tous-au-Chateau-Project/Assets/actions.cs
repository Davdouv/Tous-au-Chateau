using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actions : MonoBehaviour {

    public ResourceManager resourceM;
    public GameObject spawnPoint;
    public GameObject bridge;
    public GameObject wall;

    VRTK.VRTK_ControllerEvents events;
    bool trigger;
    bool crushMode;
    bool haveBuilding;
    Vector3 currentPos;

    GameObject newBuilding;

    // Use this for initialization
    void Start () {
        events = GetComponent<VRTK.VRTK_ControllerEvents>();
        trigger = false;
        crushMode = false;
        haveBuilding = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (events.triggerPressed)
        {
            if (!trigger)
            {
                currentPos = gameObject.transform.position;
            }
            trigger = true;
        }
        else
        {
            trigger = false;
            crushMode = false;
            if (haveBuilding)
            {
                //releaseBuilding
                haveBuilding = false;
                newBuilding.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if (trigger)
        {
            //currentPos = gameObject.transform.position;
            if(gameObject.transform.position.y < currentPos.y - 15)
            {
                crushMode = true;
            }
        }

        if (haveBuilding)
        {
            newBuilding.transform.position = spawnPoint.transform.position;
            newBuilding.transform.localRotation = spawnPoint.transform.localRotation;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (crushMode) //Destroy element of the nature
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

        //For buildings
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (events.triggerPressed && !haveBuilding)
        {
            if (other.tag == "bridge")
            {
                if (resourceM.RemoveWood(5))
                {
                    newBuilding = Instantiate(bridge, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    haveBuilding = true;
                }
            }
            else if (other.tag == "wall")
            {
                if (resourceM.RemoveStone(5))
                {
                    newBuilding = Instantiate(wall, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    haveBuilding = true;
                }
            }
        }
    }

    //void OnTrigg
}
