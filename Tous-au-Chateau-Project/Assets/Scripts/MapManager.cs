using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {

    private static MapManager _instance;

    public GameObject _TreesPosition;

    // ***** SINGLETON *****/
    public static MapManager Instance
    {
        get
        {
            // create logic to create the instance
            if (_instance == null)
            {
                GameObject go = new GameObject("_MapManager");
                go.AddComponent<MapManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Generate();
    }


    public void Generate()
    {
        for (int i = 0; i < _TreesPosition.transform.childCount; i++)
        {
            GameObject go = this.GetComponent<TreeManager>().BuildTree();
            go.transform.position = _TreesPosition.transform.GetChild(i).gameObject.transform.position;

        }
    }
}
