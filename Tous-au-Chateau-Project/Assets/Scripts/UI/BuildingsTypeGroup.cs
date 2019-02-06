using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingsTypeGroup : MonoBehaviour {

    private BuildingsTypeGroup _instance;

    public List<Building> _buildings = new List<Building>();

    public void  instance(BuildingsTypeGroup _group)
    {
        _instance = _group;
    }

    public List<List<Building>> getBuildingsSortedByType()
    {
        List<List<Building>> sortedList = new List<List<Building>>();

        foreach(BuildingType bt in (BuildingType[]) Enum.GetValues(typeof(BuildingType)))
        {
            List<Building> btTypeResult = _buildings.FindAll(
                delegate (Building b)
                {
                    return b.buildingType == bt;
                }
            );

            if (btTypeResult != null)
            {
                sortedList.Add(btTypeResult);
            }
        }

        return sortedList;
    }
}
