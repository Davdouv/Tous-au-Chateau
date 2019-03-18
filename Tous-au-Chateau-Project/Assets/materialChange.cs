using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialChange : MonoBehaviour
{

    public Material transparent_mat;
    public Material transparentRed_mat;
    Renderer rend;
    public bool inCollision;
    private bool _mustChange = false;

    protected List<GameObject> _targetList = new List<GameObject>();

    void Start()
    {
        rend = GetComponent<Renderer>();
        inCollision = false;
    }

    void Update()
    {
        if (_mustChange)
        {
            if (inCollision)
            {
                //rend.material = transparentRed_mat;
                ChangeMaterial(transparentRed_mat);
            }
            else
            {
                //rend.material = transparent_mat;
                ChangeMaterial(transparent_mat);
            }
        }
    }

    // ***** DETECTION *****/
    // Trigger must be enabled on collider
    private void OnTriggerEnter(Collider other)
    {
        // Store the targets
        _targetList.Add(other.gameObject);
        Debug.Log("Add : " + other.gameObject.name);
    }
    private void OnTriggerExit(Collider other)
    {
        // Remove the targets
        RemoveTarget(other.gameObject);
    }
    protected void RemoveTarget(GameObject target)
    {
        foreach (GameObject tar in _targetList)
        {
            if (tar == target)
            {
                Debug.Log("Remove : " + target.name);
                _targetList.Remove(tar);
                return;
            }
        }
    }
    // Check distance between this & a target
    private bool IsInRange(Vector3 position, float distanceDetection = 1f)
    {
        Vector3 closestPoint = GetComponent<BoxCollider>().ClosestPoint(position);
        float distance = (closestPoint - position).sqrMagnitude;
        return (distance < distanceDetection); // Detect if the given position is inside the sphere collider
    }

    // Check distance with all targets & test if we have to change material
    private void FixedUpdate()
    {
        bool wasInCollision = inCollision;
        bool inContact = false;
        for (int i = 0; i < _targetList.Count && !inContact; ++i)
        {
            if (_targetList[i] != null)
            {
                // If it's the terrain, it's in contact
                if (_targetList[i].tag == "Ground")
                {
                    inContact = true;
                    break;
                }
                BoxCollider box = _targetList[i].GetComponent<BoxCollider>();
                if (box)
                {
                    // Check distance with closest point
                    if (IsInRange(box.ClosestPoint(transform.position)))
                    {
                        inContact = true;
                    }
                }
                else
                {
                    // Check distance with position
                    if (IsInRange(_targetList[i].transform.position))
                    {
                        inContact = true;
                    }
                }
            }
        }
        inCollision = inContact;
        if (wasInCollision != inCollision)
        {
            _mustChange = true;
        }
        else
        {
            _mustChange = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TOTOTO");
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (IsInRange(other.transform.position))
        {
            inCollision = true;
        }
        /*}
        else
        {
            InCollision = false;
        }*/
    }

    void ChangeMaterial(Material newMat)
    {
        Debug.Log("Change Material");
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
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
}