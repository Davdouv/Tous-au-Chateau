using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButtonScript : MonoBehaviour
{
    public SpeechEvent_MapSelector_CreditEvent creditevent; // credits objects
    
    public void OpenCreditEven()
    {
        Launch();
        creditevent.hasPressed = true;
        ((SpeechEvent_MapSelector_Event1)(creditevent.previousEvent)).canClose = true;
    }
    private void Launch()
    {
        GetComponent<Animator>().enabled = true;
    }
    public void Stop()
    {
        GetComponent<Animator>().enabled = false;
    }


}
