using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButtonScript : MonoBehaviour
{
    public Transform credits; // credits objects
    
    public void OnCollisionEnter(Collision collision)
    {
        Launch();
        credits.gameObject.SetActive(true);
        credits.GetComponent<CreditAnimation>().Launch();
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
