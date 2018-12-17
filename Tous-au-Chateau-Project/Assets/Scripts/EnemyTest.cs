using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour {
    
    private AudioSource crushAudio;
    
	void Start () {
        crushAudio = gameObject.GetComponent<AudioSource>();
    }

    // ***** CALLED ON COLLISION WITH HAND *****/
    // Play Sound
    // Do other stuff...
    public void CrushDown()
    {
        crushAudio.Play();
        Debug.Log("Crush Down !");
    }
}
