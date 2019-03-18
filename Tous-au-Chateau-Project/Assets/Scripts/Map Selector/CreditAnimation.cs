using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditAnimation : MonoBehaviour {

    public Transform player;
	public void Launch()
    {
        GetComponent<Animator>().enabled = true ;
    }
    public void Stop()
    {
        GetComponent<Animator>().enabled = false;
        gameObject.SetActive(false);

    }
    private void Update()
    {
        
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
