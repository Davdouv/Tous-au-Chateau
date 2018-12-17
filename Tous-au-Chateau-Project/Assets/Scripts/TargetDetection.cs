using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour {

    public List<string> targetTag = new List<string>();

	private GameObject _target;
    private bool _isInContact;

    // ***** DETECTION *****/
    private void OnTriggerEnter(Collider other)
    {
        // If the target's tag is in the list
        if (targetTag.Contains(other.gameObject.tag))
        {
            _target = other.gameObject;

            Debug.Log("Target in sight");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (targetTag.Contains(other.gameObject.tag))
        {
            _target = null;

            Debug.Log("Target got away");
        }
    }

    // ***** COLLISION ***** //
    private void OnCollisionEnter(Collision collision)
    {
        if (targetTag.Contains(collision.gameObject.tag))
        {
            _isInContact = true;

            Debug.Log("Collision !");

            collision.gameObject.GetComponent<EnemyTest>().CrushDown();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (targetTag.Contains(collision.gameObject.tag))
        {
            _isInContact = false;

            Debug.Log("Collision no more");
        }
    }

    public bool IsInContact()
    {
        return _isInContact;
    }

    public GameObject GetTarget()
    {
        return _target;
    }
}
