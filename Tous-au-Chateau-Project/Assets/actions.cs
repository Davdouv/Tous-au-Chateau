using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actions : MonoBehaviour {

    public ResourceManager resourceM;
    public GameObject spawnPoint;
    public GameObject bridge;
    public GameObject wall;
    public Transform RightHand;

    VRTK.VRTK_ControllerEvents events;
    bool trigger;
    bool crushMode;
    bool haveBuilding;
    bool handStillClose;
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
            handStillClose = false;
            if (haveBuilding)
            {
                //releaseBuilding
                haveBuilding = false;
                newBuilding.GetComponent<Rigidbody>().isKinematic = false;
                newBuilding.transform.parent = null;
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
            newBuilding.transform.SetParent(RightHand);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (crushMode && !handStillClose) //Destroy element of the nature
        {
            if (other.tag == "Wood")
            {
                //other.crush()   <= for the next sprint
                Debug.Log("INTO WOOD");
                resourceM.AddResources(new ResourcesPack { wood = 1 });
                Destroy(other.gameObject);
                handStillClose = true;
            }
            else if (other.tag == "Stone")
            {
                Debug.Log("INTO STONE");
                resourceM.AddResources(new ResourcesPack { stone = 1 });
                Destroy(other.gameObject);
                handStillClose = true;
            }
            else if (other.tag == "Food")
            {
                Debug.Log("INTO FOOD");
                resourceM.AddResources(new ResourcesPack { food = 1 });
                Destroy(other.gameObject);
                handStillClose = true;
            }
        }

        //For buildings
        /*
     

                //Contruction
                if (objectToGrab.gameObject.CompareTag("bridge") && canConstructBridge)
                {
                    
                }
                else if (objectToGrab.gameObject.CompareTag("wall") && canConstructWall)
                {
                    if (ressourceM.RemoveResources(new ResourcesPack { stone = 5 }))
                    {
                        objectToGrab.GetComponent<Rigidbody>().isKinematic = false;
                        objectToGrab.transform.SetParent(theScene);
                        objectToGrab.gameObject.tag = "crushable";
                        GameObject myNewCube = Instantiate(wall, initialWallTrans.position, initialWallTrans.rotation);
                        VRTK_InteractableObject objScript = myNewCube.GetComponent<VRTK_InteractableObject>();
                        objScript.isGrabbable = false;
                        myNewCube.GetComponent<Rigidbody>().isKinematic = true;
                        myNewCube.transform.SetParent(UI);
                        canConstructWall = false;
                    }
                }

         * 
         * 
         */

    }

    private void OnTriggerStay(Collider other)
    {
        if (events.triggerPressed && !haveBuilding)
        {
            if (other.tag == "bridge")
            {
                if (resourceM.RemoveResources(new ResourcesPack { wood = 5 }))
                {
                    newBuilding = Instantiate(bridge, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    haveBuilding = true;
                }
            }
            else if (other.tag == "wall")
            {
                if (resourceM.RemoveResources(new ResourcesPack { stone = 5 }))
                {
                    newBuilding = Instantiate(wall, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    haveBuilding = true;
                }
            }
        }
    }

    //void OnTrigg
}
