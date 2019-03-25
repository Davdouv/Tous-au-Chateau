using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainActions : MonoBehaviour
{
    public ResourceManager resourceM;
    public UIManager uiM;
    public GameObject spawnPoint;
    public Transform RightHand;

    public VRTK.VRTK_ControllerEvents events;
    bool trigger;
    bool crushMode;
    bool haveBuilding;
    bool haveVillager;
    bool canCrush;
    Vector3 currentPos;
    public float minHeightToCrush = 7;
    private AudioSource _audioData;
    public AudioClip crushFloorSound;

    private SphereCollider sphereCollider;
    private float distanceDetection;

    GameObject newBuilding;
    GameObject buildingPreview;
    GameObject newVillager;
    GameObject oldVillager;
    public GameObject villagerPrefab;
    GameObject buildingPrefab;
    GameObject buildingPreviewPrefab;

    public GameObject fxPrefab;
    public GameObject impactPreview;

    public SpeechEvent_MapTuto1_Event1 speechEvent1 = null;
    public SpeechEvent_MapTuto1_Event2 speechEvent2 = null;
    public SpeechEvent_MapTuto1_Event4_1 speechEvent4_1 = null;
    public SpeechEvent_MapTuto1_Event7 speechEvent7 = null;
    public SpeechEvent_MapTuto2_Event1 speechEvent2_1 = null;
    public SpeechEvent_MapTuto3_Event1 speechEvent3_1 = null;

    Material[] mats;
    string[] objName;

    public GameObject player;

    private bool mapManager = false;

    // Use this for initialization
    void Start()
    {
        //events = GetComponent<VRTK.VRTK_ControllerEvents>();
        trigger = false;
        crushMode = false;
        haveBuilding = false;
        _audioData = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        distanceDetection = sphereCollider.radius * 100 * 100; // 100 is the scale of the last parent (other parent has scale of 1) and 100 of the game object

        if (player == null)
        {
            player = GameObject.Find("[VRTK_SDKManager]");
        }

        mapManager = (SceneManager.GetActiveScene().name == "Map Selector");
        impactPreview.SetActive(false);
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
                VerifyActionTuto(speechEvent2_1);
                VerifyActionTuto(speechEvent3_1);
            }
        }

        //Control height of the map
        if (events.touchpadPressed)
        {
            Vector2 touchPosition;
            touchPosition = events.GetTouchpadAxis();
            if (touchPosition.y > 0.5f)
            {
                //Move table up
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 5, player.transform.position.z);
            }
            else if (touchPosition.y < 0.5f)
            {
                //Move table down
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z);
            }
        }

        if (events.triggerPressed)
        {
            if (!trigger || gameObject.transform.position.y > currentPos.y)
            {
                currentPos = gameObject.transform.position;
            }
            trigger = true;

            if (!haveBuilding)
            {
                // Preview the crush position into the ground
                impactPreview.SetActive(ShowCrushPreview());
            }
            else
            {
                ShowConstructionPreview();
            }
        }
        else
        {
            trigger = false;
            crushMode = false;
            canCrush = true;
            if (haveBuilding)
            {
                if (buildingPreview.GetComponent<materialChange>().inCollision)
                {
                    //releaseBuilding
                    haveBuilding = false;
                    //EnableBoxColliders(newBuilding, true);
                    newBuilding.GetComponent<Rigidbody>().isKinematic = false;
                    newBuilding.transform.parent = null;
                    //On hand release
                    Transform buildingTrans;
                    buildingTrans = buildingPreview.transform;
                    Destroy(buildingPreview);
                    Destroy(newBuilding);
                    newBuilding = Instantiate(buildingPrefab, buildingTrans);
                    EnableBoxColliders(newBuilding, true);
                }
                else
                {
                    haveBuilding = false;
                    Destroy(buildingPreview);
                    Destroy(newBuilding);
                    //We need to check ressources to be able to retrieve a pack when not used
                }
            }
            else if (SceneManager.GetActiveScene().name == "Map Selector")
            {
                if (haveVillager)
                {
                    //releaseVillager
                    haveVillager = false;
                    //On hand
                    oldVillager.SetActive(true);
                    var table = GameObject.Find("Table");
                    newVillager.transform.SetParent(table.transform);
                    newVillager.GetComponent<Rigidbody>().isKinematic = false;
                    // Destroy(newVillager);
                }
            }
            impactPreview.SetActive(false);
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
        Vector3 handCenter = MiddleOfHand();
        float distance = (handCenter - position).sqrMagnitude;
        return (distance < distanceDetection * distanceDetection); // Detect if the given position is inside the sphere collider
    }

    private void OnTriggerEnter(Collider other)
    {
        CrushGround(other);
    }

    private void CrushGround(Collider other)
    {
        if (crushMode && canCrush && !haveBuilding)
        {
            if (other.gameObject.tag == "Ground")
            {
                // Sound
                _audioData.clip = crushFloorSound;
                _audioData.Play();

                // FX
                Vector3 handCenter = MiddleOfHand();
                handCenter.y -= distanceDetection / 2;
                Instantiate(fxPrefab, handCenter, transform.rotation).SetActive(true);

                // Shake
                CameraManager.Instance.ShakeCamera();

                if (GameManager.Instance.tuto)
                {
                    if (speechEvent1)
                    {
                        speechEvent1.hasCrushedGround = true;
                    }                    
                }
            }
        }
    }

    // Return true if we crushed something
    private bool CrushAction(Collider other)
    {
        if (crushMode && canCrush && !haveBuilding) //Destroy element of the nature
        {
            if (other.gameObject.GetComponent<Crushable>() && other.gameObject.GetComponent<Crushable>().canBeCrushed)
            {
                if (IsInRange(other.transform.position))
                {
                    // Play the sound of the object we are going to destroy
                    _audioData.clip = other.gameObject.GetComponent<Crushable>().GetClip();
                    _audioData.Play();

                    resourceM.AddResources(other.gameObject.GetComponent<Crushable>().Gain());
                    other.gameObject.GetComponent<Crushable>().Crush();

                    canCrush = false;
                    return true;
                }
            }
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (mapManager)
        {
            if (events.triggerPressed && !haveVillager)
            {
                if (other.tag == "Villager")
                {
                    if (other.name == "villager")
                    {
                        oldVillager = other.gameObject;
                        oldVillager.SetActive(false);
                        newVillager = Instantiate(villagerPrefab, spawnPoint.transform);
                    } else
                    {
                        newVillager.transform.SetParent(spawnPoint.transform);
                        newVillager.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    //other.gameObject.transform = spawnPoint.transform;
                    newVillager.transform.localPosition = new Vector3(0.0516f, 0.0184f, -0.0194f);
                    newVillager.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
                    // newVillager.GetComponent<Rigidbody>().enabled = false;
                    newVillager.SetActive(true);
                    haveVillager = true;
                }

            }
        }
        // If we didn't crush, check for other actions
        else if (!CrushAction(other))
        {
            if (events.triggerPressed && !haveBuilding)
            {
                if (other.tag == "Building")
                {
                    //Get ResourcePack building
                    if (other.gameObject.GetComponent<Building>().CanBuy())
                    {
                        //Instantiate building
                        buildingPrefab = other.gameObject.GetComponent<Building>().prefab;
                        newBuilding = Instantiate(other.gameObject.GetComponent<Building>().prefab, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                        buildingPreviewPrefab = other.gameObject.GetComponent<Building>().prefabTransparent;
                        EnableBoxColliders(newBuilding, false);
                        buildingPreview = Instantiate(buildingPreviewPrefab);
                        haveBuilding = true;
                    }
                }
                else if (other.name == "Construction Panel Button 0")
                {
                    {
                        //Change to page 1 on UI
                        uiM.DisplayConstructionPage(0);
                    }
                }
                else if (other.name == "Construction Panel Button 1")
                {
                    {
                        //Change to page 2 on UI
                        uiM.DisplayConstructionPage(1);
                    }
                }
                else if (other.name == "Construction Panel Button 2")
                {
                    {
                        //Change to page 3 on UI
                        uiM.DisplayConstructionPage(2);
                    }
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

    private Vector3 MiddleOfHand()
    {
        return transform.TransformPoint(sphereCollider.center);
    }
    private Vector3 BorderOfHand(bool right)
    {
        return transform.TransformPoint(sphereCollider.center) + new Vector3 (right ? distanceDetection : -distanceDetection, 0, right ? distanceDetection : -distanceDetection);
    }

    // Return true if the raycast hit
    private bool ShowCrushPreview()
    {
        // Bit shift the index of the layer (10) (terrain) to get a bit mask
        int layerMask = 1 << 11;

        RaycastHit hit, hitRight, hitLeft;

        Debug.DrawRay(MiddleOfHand(), new Vector3(0, -1, 0) * 100, Color.red);
        Debug.DrawRay(BorderOfHand(true), new Vector3(0, -1, 0) * 100, Color.red);
        Debug.DrawRay(BorderOfHand(false), new Vector3(0, -1, 0) * 100, Color.red);

        bool showCrush = false;

        //raycast from center down vector
        if (Physics.Raycast(MiddleOfHand(), new Vector3(0, -1, 0), out hit, Mathf.Infinity, layerMask))
        {
            showCrush = true;
        }
        //raycast from border and diagonal
        if (Physics.Raycast(BorderOfHand(true), new Vector3(0, -1, 0), out hitRight, Mathf.Infinity, layerMask))
        {
            showCrush = true;
        }
        //raycast from other border and diagonal
        if (Physics.Raycast(BorderOfHand(false), new Vector3(0, -1, 0), out hitLeft, Mathf.Infinity, layerMask))
        {
            showCrush = true;
        }

        if (showCrush)
        {
            float HighestHit = Mathf.Min(hit.distance, hitRight.distance, hitLeft.distance);
            impactPreview.transform.position = MiddleOfHand() + (new Vector3(0, -HighestHit + 0.3f, 0));
        }
        return showCrush;
    }

    private void ShowConstructionPreview()
    {
        int layerMask = 1 << 11;

        RaycastHit hit;

        Debug.DrawRay(MiddleOfHand(), new Vector3(0, -1, 0) * 100, Color.red);
        //newBuilding.transform.position;
        if (Physics.Raycast(MiddleOfHand(), new Vector3(0, -1, 0), out hit, Mathf.Infinity, layerMask))
        {
            Vector3 previewPosition = MiddleOfHand() + (new Vector3(0, -hit.distance + 0.3f, 0));
            buildingPreview.transform.position = previewPosition;

            var angles = newBuilding.transform.rotation.eulerAngles;
            angles.x = -90;
            angles.y = 90;
            buildingPreview.transform.rotation = Quaternion.Euler(angles);
        }
    }

    private void EnableBoxColliders(GameObject gameObject, bool enable)
    {
        gameObject.GetComponent<BoxCollider>().enabled = enable;
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            if (gameObject.transform.GetChild(i).GetComponent<BoxCollider>())
            {
                gameObject.transform.GetChild(i).GetComponent<BoxCollider>().enabled = enable;
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
