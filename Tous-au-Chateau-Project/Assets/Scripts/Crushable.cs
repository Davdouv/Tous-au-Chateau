using System.Collections;
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
            CameraManager.Instance.ShakeCamera();

            if (crushFXPrefab)
            {
                Debug.Log("FX");
                GameObject fx = Instantiate(crushFXPrefab, transform);
                fx.SetActive(true);
                fx.transform.SetParent(transform.parent.parent);
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
