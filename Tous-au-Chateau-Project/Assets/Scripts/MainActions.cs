﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainActions : MonoBehaviour
{
    public ResourceManager resourceM;
    public UIManager uiM;
    public GameObject spawnPoint;
    public Transform RightHand;

    VRTK.VRTK_ControllerEvents events;
    bool trigger;
    bool crushMode;
    bool haveBuilding;
    bool haveVillager;
    bool canCrush;
    Vector3 currentPos;
    public float minHeightToCrush = 10;
    private AudioSource _audioData;
    public AudioClip crushFloorSound;

    private SphereCollider sphereCollider;
    private float distanceDetection;


    public Material Transparent_Building;
    Material Building_mat;

    GameObject newBuilding;
    GameObject newVillager;
    GameObject oldVillager;
    public GameObject villagerPrefab;
    GameObject buildingPrefab;

    public GameObject fxPrefab;


    public SpeechEvent_MapTuto1_Event1 speechEvent1 = null;
    public SpeechEvent_MapTuto1_Event2 speechEvent2 = null;
    public SpeechEvent_MapTuto1_Event4_1 speechEvent4_1 = null;
    public SpeechEvent_MapTuto1_Event7 speechEvent7 = null;

    Material[] mats;
    string[] objName;

    public GameObject player;

    // Use this for initialization
    void Start()
    {
        events = GetComponent<VRTK.VRTK_ControllerEvents>();
        trigger = false;
        crushMode = false;
        haveBuilding = false;
        _audioData = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        distanceDetection = sphereCollider.radius * 100; // 100 is the scale of the last parent (other parent has scale of 1)

        if (player == null)
        {
            player = GameObject.Find("[VRTK_SDKManager]");
        }
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

        //Control height of the map 
        if (events.touchpadPressed)
        {
            Vector2 touchPosition;
            touchPosition = events.GetTouchpadAxis();
            if(touchPosition.y > 0.5f)
            {
                //Move table up
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 10, player.transform.position.z);
            }
            else if(touchPosition.y < 0.5f)
            {
                //Move table down
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 10, player.transform.position.z);
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
            canCrush = true;
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
                newBuilding = Instantiate(buildingPrefab, buildingTrans.position, buildingTrans.rotation);
            }
            else if (SceneManager.GetActiveScene().name == "Map selector")
            {
                if (haveVillager)
                {
                    //releaseVillager
                    haveVillager = false;
                    //On hand release
                    oldVillager.SetActive(true);
                    Destroy(newVillager);
                }
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
                canCrush = true;
            }
        }

        if (haveBuilding)
        {
            newBuilding.transform.position = spawnPoint.transform.position;
            newBuilding.transform.localRotation = spawnPoint.transform.localRotation;
            newBuilding.transform.SetParent(RightHand);
        }
    }

    private bool IsInRange(Vector3 position)
    {
        Debug.Log("Target position : " + position);
        Debug.Log("Center position : " + sphereCollider.center);
        Debug.Log("DistanceDetection : " + distanceDetection);
        float distance = (sphereCollider.center - position).sqrMagnitude;
        Debug.Log("Distance : " + distance);
        return (distance < distanceDetection * distanceDetection);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (crushMode && canCrush) //Destroy element of the nature
        {
            if (other.gameObject.GetComponent<Crushable>() && IsInRange(other.ClosestPoint(sphereCollider.center)))
            {
                resourceM.AddResources(other.gameObject.GetComponent<Crushable>().Gain());
                other.gameObject.GetComponent<Crushable>().Crush();

                if (other.gameObject.GetComponent<CharacterStats>())
                {
                    if (other.gameObject.GetComponent<CharacterStats>().IsAlive())
                    {
                        // Play sound of ai character dying
                        _audioData.clip = other.gameObject.GetComponent<AudioClip>();
                        _audioData.Play(0);
                    }
                }
                else
                {
                    // Play sound of resource destroyed
                    _audioData.clip = other.gameObject.GetComponent<AudioClip>();
                    _audioData.Play();
                }
                canCrush = false;
            }
            else
            {
                // Sound
                _audioData.clip = crushFloorSound;
                _audioData.Play(0);

                // FX
                Instantiate(fxPrefab, transform).SetActive(true);
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
                    buildingPrefab = other.gameObject.GetComponent<Building>().prefab;
                    haveBuilding = true;
                }
            }
            else if (other.name == "Construction Panel Button 0")
            {
                {
                    //Change to page 1 on UI
                    //Call function from @justine script
                    uiM.DisplayConstructionPage(0);
                }
            }
            else if (other.name == "Construction Panel Button 1")
            {
                {
                    //Change to page 2 on UI
                    //Call function from @justine script
                    uiM.DisplayConstructionPage(1);
                }
            }
            else if (other.name == "Construction Panel Button 2")
            {
                {
                    //Change to page 3 on UI
                    //Call function from @justine script
                    uiM.DisplayConstructionPage(2);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Map selector")
        {
            if (events.triggerPressed && !haveVillager)
            {
                if (other.tag == "Villager")
                {
                    //other.gameObject.transform = spawnPoint.transform;
                    oldVillager = other.gameObject;
                    oldVillager.SetActive(false);
                    newVillager = Instantiate(villagerPrefab, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    haveVillager = true;
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
