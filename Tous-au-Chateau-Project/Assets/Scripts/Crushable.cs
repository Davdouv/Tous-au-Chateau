using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushable : MonoBehaviour {

    [SerializeField]     protected ResourcesPack _gain;

    // Can be set to false for some reasons in tutorial for example
    public bool canBeCrushed = true;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
    public ResourcesPack Gain()
    {
        return _gain;
    }

	public void Crush()
    {
        if (canBeCrushed)
        {
            // If it's a character, make him die
            if (this.GetComponent<CharacterStats>())
            {
                this.GetComponent<CharacterStats>().TakeDamage(9999, DeathReason.PLAYER);
            }
            // Destroy the object
            else
            {
                Destroy(gameObject);
            }
        }              
    }
}
