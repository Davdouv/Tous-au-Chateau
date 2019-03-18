using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButtonScript : MonoBehaviour
{
    public SpeechEvent creditevent; // credits objects
    
    public void OnCollisionEnter(Collision collision)
    {
        creditevent.MustOpen();
    }
    public void Launch()
    {
        GetComponent<Animator>().enabled = true;
    }
    public void Stop()
    {
        GetComponent<Animator>().enabled = false;
    }


}
