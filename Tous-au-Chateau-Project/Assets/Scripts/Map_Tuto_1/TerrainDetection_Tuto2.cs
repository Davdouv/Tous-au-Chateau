using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection_Tuto2 : MonoBehaviour {

    public SpeechEvent_MapTuto2_Event4 speechEvent4;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10) // 10 : Building
        {
            collision.gameObject.GetComponent<Building>().SetHasLanded();
            if (collision.gameObject.GetComponent<StoneWall>())
            {
                speechEvent4.hasStoneWallLanded = true;
            }
        }
    }
}
