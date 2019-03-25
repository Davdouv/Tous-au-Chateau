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

    public CreditButtonScript creditButton;
    public ExitButtonScript exitButton;

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
            // If we had a building and we have released it
            if (haveBuilding)
            {
                impactPreview.SetActive(false);

                //releaseBuilding
                haveBuilding = false;
                // If the Preview Was In Collision, then don't create new building
                if (buildingPreview.GetComponent<materialChange>().inCollision)
                {
                    ResourceManager.Instance.AddResources(newBuilding.GetComponent<Building>().getCost());
                    Destroy(newBuilding);
                }
                // If it wasn't in collision, then create new building
                else
                {
                    //newBuilding = Instantiate(buildingPrefab, buildingTrans);
                    newBuilding.GetComponent<Rigidbody>().isKinematic = false;
                    newBuilding.transform.parent = null;

                    // The building we had in the hand take the position & rotation of the preview
                    newBuilding.transform.position = buildingPreview.transform.position;
                    newBuilding.transform.rotation = buildingPreview.transform.rotation;
                    EnableBoxColliders(newBuilding, true);
                }
                Destroy(buildingPreview);
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

    private void PushButtons()
    {
        if (crushMode && canCrush && !haveVillager)
        {
            if (creditButton && IsInRange(creditButton.gameObject.transform.position))
            {
                creditButton.OpenCreditEven();
                return;
            }
            if (exitButton && IsInRange(exitButton.gameObject.transform.position))
            {
                exitButton.ExitGame();
            }
        }            
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
            PushButtons();
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
                        impactPreview.SetActive(false);
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

    // Test if an object ray cast hit the terrain
    private bool RayCastHit(Vector3 middle, Vector3 right, Vector3 left, ref float highestHit)
    {
        int layerMask = 1 << 11;

        RaycastHit hit, hitRight, hitLeft;

        Debug.DrawRay(middle, new Vector3(0, -1, 0) * 100, Color.red);
        Debug.DrawRay(right, new Vector3(0, -1, 0) * 100, Color.red);
        Debug.DrawRay(left, new Vector3(0, -1, 0) * 100, Color.red);

        bool middleHit = false;
        bool rightHit = false;
        bool leftHit = false;
        bool showCrush = false;

        //raycast from center down vector
        if (Physics.Raycast(middle, new Vector3(0, -1, 0), out hit, Mathf.Infinity, layerMask))
        {
            middleHit = true;
        }
        //raycast from border and diagonal
        if (Physics.Raycast(right, new Vector3(0, -1, 0), out hitRight, Mathf.Infinity, layerMask))
        {
            rightHit = true;

        }
        //raycast from other border and diagonal
        if (Physics.Raycast(left, new Vector3(0, -1, 0), out hitLeft, Mathf.Infinity, layerMask))
        {
            leftHit = true;
        }

        if (middleHit || rightHit || leftHit)
        {
            showCrush = true;

            if (middleHit && rightHit && leftHit)
            {
                highestHit = Mathf.Min(hit.distance, hitRight.distance, hitLeft.distance);
            }
            else if (middleHit && rightHit)
            {
                highestHit = Mathf.Min(hit.distance, hitRight.distance);
            }
            else if (middleHit && leftHit)
            {
                highestHit = Mathf.Min(hit.distance, hitLeft.distance);
            }
            else if (rightHit && leftHit)
            {
                highestHit = Mathf.Min(hitRight.distance, hitLeft.distance);
            }
            else if (middleHit)
            {
                highestHit = hit.distance;
            }
            else if (rightHit)
            {
                highestHit = hitRight.distance;
            }
            else if (leftHit)
            {
                highestHit = hitLeft.distance;
            }
        }

        return showCrush;
    }

    // Return true if the raycast hit
    private bool ShowCrushPreview()
    {
        float highestHit = 0;
        bool showCrush = RayCastHit(MiddleOfHand(), BorderOfHand(true), BorderOfHand(false), ref highestHit);

        if (showCrush)
        {
            impactPreview.transform.position = MiddleOfHand() + (new Vector3(0, -highestHit + 0.3f, 0));
        }

        return showCrush;
    }

    private void ShowConstructionPreview()
    {
        BoxCollider boxCollider = buildingPreview.GetComponent<BoxCollider>();
        Vector3 scale = buildingPreview.transform.localScale;

        Vector3 middlePosition = newBuilding.transform.position;
        Vector3 rightPosition = new Vector3(middlePosition.x + (boxCollider.size.x * scale.x), middlePosition.y, middlePosition.z + (boxCollider.size.y / scale.y));
        Vector3 leftPosition = new Vector3(middlePosition.x - (boxCollider.size.x * scale.x), middlePosition.y, middlePosition.z - (boxCollider.size.y / scale.y));

        float highestHit = 0;
        if (RayCastHit(middlePosition, rightPosition, leftPosition, ref highestHit))
        {
            // Project building Preview onto the terrain
            float heightOffset = 0.5f;
            Vector3 previewPosition = newBuilding.transform.position + (new Vector3(0, -highestHit + heightOffset, 0));
            // Copy X & Z position
            buildingPreview.transform.position = previewPosition;
            // Copy Y rotation
            float yRotation = newBuilding.transform.eulerAngles.y;
            buildingPreview.transform.eulerAngles = new Vector3(buildingPreview.transform.eulerAngles.x, yRotation, buildingPreview.transform.eulerAngles.z);
        }
    }

    private void EnableBoxColliders(GameObject gameObject, bool enable)
    {
        if (gameObject.transform.GetComponent<BoxCollider>())
        {
            gameObject.GetComponent<BoxCollider>().enabled = enable;
        }
            
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            EnableBoxColliders(gameObject.transform.GetChild(i).gameObject, enable);
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
