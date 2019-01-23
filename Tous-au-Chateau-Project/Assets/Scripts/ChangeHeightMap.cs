using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeHeightMap : MonoBehaviour {

    public Terrain terrain;
    public GameObject map;

    //public VRTK_ControllerEvents leftControllerEvents;
    //public VRTK_ControllerEvents rightControllerEvents;

    public void Increase()
    {
        terrain.transform.position = new Vector3(terrain.transform.position.x, terrain.transform.position.y + 0.01f, terrain.transform.position.z);
        map.transform.position = new Vector3(map.transform.position.x, map.transform.position.y + 0.01f, map.transform.position.z);
    }

    public void Decrease()
    {
        terrain.transform.position = new Vector3(terrain.transform.position.x, terrain.transform.position.y - 0.01f, terrain.transform.position.z);
        map.transform.position = new Vector3(map.transform.position.x, map.transform.position.y - 0.01f, map.transform.position.z);
    }
}
