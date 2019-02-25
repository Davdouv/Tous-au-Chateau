using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainActions : MonoBehaviour {

    public ResourceManager resourceM;
    public GameObject spawnPoint;
    public Transform RightHand;

    VRTK.VRTK_ControllerEvents events;
    bool trigger;
    bool crushMode;
    bool haveBuilding;
    bool handStillClose;
    Vector3 currentPos;


    GameObject newBuilding;

    public SpeechEvent_MapTuto1_Event1 speechEvent1 = null;
    public SpeechEvent_MapTuto1_Event2 speechEvent2 = null;
    public SpeechEvent_MapTuto1_Event4_1 speechEvent4_1 = null;
    public SpeechEvent_MapTuto1_Event7 speechEvent7 = null;


    // Use this for initialization
    void Start () {
        events = GetComponent<VRTK.VRTK_ControllerEvents>();
        trigger = false;
        crushMode = false;
        haveBuilding = false;
	}

	// Update is called once per frame
	void Update () {

        if (GameManager.Instance.tuto)
        {
            if (events.triggerPressed || events.touchpadPressed)
            {
                VerifyActionTuto(speechEvent2);
                VerifyActionTuto(speechEvent4_1);
                VerifyActionTuto(speechEvent7);
            }
        }

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
        //Debug.Log(other.tag);
        if (crushMode && !handStillClose) //Destroy element of the nature
        {
            if (other.tag == "Crushable")
            {
                //other.crush()   <= for the next sprint
                Debug.Log("CRUSH ITEM");
                resourceM.AddResources(other.gameObject.GetComponent<Crushable>().Gain());
                other.gameObject.GetComponent<Crushable>().Crush();
                handStillClose = true;
            }

            if (GameManager.Instance.tuto)
            {
              if (other.gameObject.tag == "Ground")
              {
                speechEvent1.hasCrushedGround = true;
              }
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (events.triggerPressed && !haveBuilding)
        {
            if (other.tag == "Building")
            {
                //Get ResourcePack building
                if (other.gameObject.GetComponent<Building>().CanBuy())
                {
                    //Instantiate building
                    newBuilding = Instantiate(other.gameObject.GetComponent<Building>().prefab, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    haveBuilding = true;
                }
            }
        }
    }

    public bool IsCrushModeActive()
    {
        return crushMode;
    }

    private void VerifyActionTuto(SpeechEvent speechEvent)
    {
        if (speechEvent)
        {
            if (speechEvent.IsOpen() && speechEvent.bubble.canClose && !speechEvent.hasDoneAction)
            {
                speechEvent.hasDoneAction = true;
            }
        }
    }
}
