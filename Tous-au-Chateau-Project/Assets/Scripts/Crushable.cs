using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushable : MonoBehaviour {

    [SerializeField]     protected ResourcesPack _gain;
    private AudioSource _audioData;
    public AudioClip crushedDownSound;

    // Can be set to false for some reasons in tutorial for example
    public bool canBeCrushed = true;

    // Use this for initialization
    void Start () {
        _audioData = GetComponent<AudioSource>();
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
            // Play sound
            _audioData.clip = crushedDownSound;
            _audioData.Play(0);

            // If it's a character, make him die
            if (this.GetComponent<CharacterStats>())
            {
                this.GetComponent<CharacterStats>().TakeDamage(9999, DeathReason.PLAYER);
                if (this.GetComponent<AICharacter>())
                {
                    Destroy(gameObject);
                }
            }
            // Destroy the object
            else
            {                
                Destroy(gameObject);
            }
        }              
    }
}
