using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour {

    public SpeechEvent_MapTuto2_Event4_1 speechEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            speechEvent.isStoneWallPlacedWell = true;
        }
    }
}
