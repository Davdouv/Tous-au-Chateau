﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainActions : MonoBehaviour
{

    public ResourceManager resourceM;
    public GameObject spawnPoint;
    public Transform RightHand;

    VRTK.VRTK_ControllerEvents events;
    bool trigger;
    bool crushMode;
    bool haveBuilding;
    bool handStillClose;
    Vector3 currentPos;
    public float minHeightToCrush = 10;
    private AudioSource _audioData;
    public AudioClip crushFloorSound;


    public Material Transparent_Building;
    Material Building_mat;

    GameObject newBuilding;
    GameObject myprefab;

    public SpeechEvent_MapTuto1_Event1 speechEvent1 = null;
    public SpeechEvent_MapTuto1_Event2 speechEvent2 = null;
    public SpeechEvent_MapTuto1_Event4_1 speechEvent4_1 = null;
    public SpeechEvent_MapTuto1_Event7 speechEvent7 = null;

    Material[] mats;
    string[] objName;

    // Use this for initialization
    void Start()
    {
        events = GetComponent<VRTK.VRTK_ControllerEvents>();
        trigger = false;
        crushMode = false;
        haveBuilding = false;
        _audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

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
                //On hand release
                Transform buildingTrans;
                buildingTrans = newBuilding.transform;
                Destroy(newBuilding);
                newBuilding = Instantiate(myprefab, buildingTrans.position, buildingTrans.rotation);
            }
        }

        if (trigger)
        {
            //currentPos = gameObject.transform.position;
            if (gameObject.transform.position.y < currentPos.y - minHeightToCrush)
            {
                crushMode = true;
            }
            else if (gameObject.transform.position.y > currentPos.y - minHeightToCrush)
            {
                crushMode = false;
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
            if (other.gameObject.GetComponent<Crushable>())
            {
                //other.crush()   <= for the next sprint
                Debug.Log("CRUSH ITEM");
                resourceM.AddResources(other.gameObject.GetComponent<Crushable>().Gain());
                other.gameObject.GetComponent<Crushable>().Crush();
                handStillClose = true;
            }
            else
            {
                _audioData.clip = crushFloorSound;
                _audioData.Play(0);
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
                    newBuilding = Instantiate(other.gameObject.GetComponent<Building>().prefabTransparent, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    myprefab = other.gameObject.GetComponent<Building>().prefab;
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

   /* void ChangeMaterial(Material newMat)
    {
        Renderer[] children;
        children = newBuilding.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (int i = 0; i < rend.materials.Length; ++i)
            {
                mats[i] = newMat;
            }
            rend.materials = mats;
        }
    }
    void StockMaterial()
    {
        Renderer[] children;
        Transform[] newBuilding;
    
        children = newBuilding.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            mats = new Material[rend.materials.Length];
            objName = new string[rend.materials.Length];
            for (int i = 0; i < rend.materials.Length; ++i)
            {
                mats[i] = GetComponent<Renderer>().material;
                objName[i] = transform.name;
            }
        }
    }

    void ApplyStockedMaterial()
    {

        Renderer[] children;
        children = newBuilding.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var othermats = new Material[rend.materials.Length];
            for (int i = 0; i < rend.materials.Length; ++i)
            {
                if(transform.name == objName[i])
                {
                    othermats[i] = mats[i];
                }
            }
            rend.materials = mats;
        }
    } */
}
