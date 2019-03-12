﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushable : MonoBehaviour {

    [SerializeField]     protected ResourcesPack _gain;
    public AudioClip crushedDownSound;

    public GameObject crushFXPrefab;

    // Can be set to false for some reasons in tutorial for example
    public bool canBeCrushed = true;

    // Update is called once per frame
    public ResourcesPack Gain()
    {
        return _gain;
    }

	public void Crush()
    {
        if (canBeCrushed)
        {
            if (crushFXPrefab)
            {
                Debug.Log("FX");
                Instantiate(crushFXPrefab, transform);
            }
            // If it's a character, make him die
            if (this.GetComponent<CharacterStats>() && this.GetComponent<CharacterStats>().IsAlive())
            {
                this.GetComponent<CharacterStats>().TakeDamage(9999, DeathReason.PLAYER);
            }
            // Destroy the object
            else
            {
                Destroy(gameObject);
            }            
            canBeCrushed = false;
        }              
    }

    // Can not be played here because object might be destroyed
    public AudioClip GetClip()
    {
        return crushedDownSound;
    }
}
