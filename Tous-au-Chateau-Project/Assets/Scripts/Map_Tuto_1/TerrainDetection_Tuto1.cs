﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection_Tuto1 : MonoBehaviour {

    public SpeechEvent_MapTuto1_Event4 speechEvent4;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10) // 10 : Building
        {
            collision.gameObject.GetComponent<Building>().SetHasLanded();
            if (collision.gameObject.GetComponent<WoodPlank>())
            {
                speechEvent4.hasWookPlankLanded = true;
            }
        }
    }
}
