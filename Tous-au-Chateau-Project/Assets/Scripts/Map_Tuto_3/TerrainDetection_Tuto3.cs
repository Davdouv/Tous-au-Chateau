using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection_Tuto3 : MonoBehaviour {

    public SpeechEvent_MapTuto3_Event3 speechEvent3;
    public SpeechEvent_MapTuto3_Event4 speechEvent4;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10) // 10 : Building
        {
            collision.gameObject.GetComponent<Building>().SetHasLanded();
            if (collision.gameObject.GetComponent<DirectionalSign>())
            {
                // Tell the event the directional panel has landed
                if (speechEvent3.hasDirectionalPanelLanded && speechEvent4.IsOpen())
                {
                    speechEvent4.hasDirectionalPanelLanded = true;
                }
                else
                {                    
                    speechEvent3.hasDirectionalPanelLanded = true;
                }                
            }
        }
    }

}

