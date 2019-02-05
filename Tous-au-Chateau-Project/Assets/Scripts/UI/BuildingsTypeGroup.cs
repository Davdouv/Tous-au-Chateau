using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsTypeGroup : MonoBehaviour {

    private BuildingsTypeGroup _instance;

    public List<Building> _buildings = new List<Building>();

    public void  instance(BuildingsTypeGroup _group)
    {
        _instance = _group;
    }

}
