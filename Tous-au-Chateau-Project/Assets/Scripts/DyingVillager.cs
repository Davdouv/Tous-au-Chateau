using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingVillager : MonoBehaviour {
    public float turningRate = 20f; // maximum turn rate in degrees per second
    private Quaternion _targetRotation = Quaternion.identity;
    //public ParticleSystem ghost; // assigned in editor
    public GameObject mesh; // assigned in editor

    private bool _startAnimation = false;
   
    // Use this for initialization
    void Start () {
        //ghost.Pause();
    }

    private void Update()
    {
        if (_startAnimation)
        {
            // Turn towards our target rotation.
            SetBlendedEulerAngles(new Vector3(90, 0, 0)); // rotation on x axis 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 4);
        }
    }

    // Turn the object smoothly
    public void SetBlendedEulerAngles(Vector3 angles)
    {
        _targetRotation = Quaternion.Euler(angles);
    }

    // Called by Die() in Villager
    public void VillagerIsDead()
    {
        Debug.Log("DEAD");
        _startAnimation = true;
        //ghost.Play();
    }
}
