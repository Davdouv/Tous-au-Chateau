using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingMaker : MonoBehaviour {
    
    public GameObject prefab_wall;
    public GameObject navmesh;

    int cpt = 0;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(prefab_wall, new Vector3(2, -3.0f, -1.0f), Quaternion.identity);
            ++cpt;
            print("Wall created n°" + cpt);
            navmesh.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
	}
}
