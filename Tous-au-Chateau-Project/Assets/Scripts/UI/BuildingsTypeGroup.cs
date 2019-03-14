using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingsTypeGroup : MonoBehaviour {

    #region Singleton
    private static BuildingsTypeGroup _instance;

    public static BuildingsTypeGroup Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("_BuildingsTypeGroup");
                go.AddComponent<BuildingsTypeGroup>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }

    #endregion

    public List<Building> _buildings = new List<Building>();
    private AudioSource _audioData;
    public AudioClip notBuyable;

    private void Start()
    {
        _audioData = GetComponent<AudioSource>();
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

    public void PlaySound()
    {
        _audioData.clip = notBuyable;
        _audioData.Play();
    }
}
