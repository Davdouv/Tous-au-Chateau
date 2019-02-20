using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10) // 10 : Building
        {
            collision.gameObject.GetComponent<Building>().SetHasLanded();
        }
    }
}
