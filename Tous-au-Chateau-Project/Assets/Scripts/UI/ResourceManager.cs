﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : PauseScript
{
    #region Singleton
    private static ResourceManager _instance;

    public static ResourceManager Instance
    {
        get
        {
            // create logic to create the instance
            if (_instance == null)
            {
                GameObject go = new GameObject("_ResourceManager");
                go.AddComponent<ResourceManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    public EndOfGameManager _EndOfGame;

    private ResourcesPack _currentResources;
    private bool _isInPause;

    void Start()
    {
        _currentResources = new ResourcesPack { motivation = 100, workForce = 10};
        _isInPause = false;
        InvokeRepeating("InGameMotivation", 0.0f, 3.0f);
    }

    private void Update()
    {
        //Need to update villagers nb from Villagers group instance
    }

    private void InGameMotivation()
    {
        if (!_isInPause)
        {
            RemoveResources(new ResourcesPack { motivation = 1 });
        }
    }

    override public void Pause()
    {
        _isInPause = true;
    }

    override public void UnPause()
    {
        _isInPause = false;
    }

    //GETTERS
    //Mainly used to help UI display
    public int GetWood()
    {
        return _currentResources.wood;
    }

    public int GetStone()
    {
        return _currentResources.stone;
    }

    public int GetFood()
    {
        return _currentResources.food;
    }

    public int GetWorkForce()
    {
        return _currentResources.workForce;
    }

    public float GetMotivation()
    {
        return _currentResources.motivation;
    }

    //ADD

    public void AddResources(ResourcesPack toAdd)
    {
        if (toAdd.wood >= 0 && toAdd.stone >= 0 && toAdd.food >= 0 && toAdd.workForce >= 0 && toAdd.motivation >= 0)
            _currentResources += toAdd;

        if (_currentResources.motivation > 100)
            _currentResources.motivation = 100;
    }

    //REMOVE
    public bool RemoveResources(ResourcesPack toRemove)
    {
        //non valid values
        if (toRemove.wood < 0 || toRemove.stone < 0 || toRemove.food < 0 || toRemove.workForce < 0 || toRemove.motivation < 0)
            return false;

        //case where not enough wood to remove
        if(toRemove.wood > 0 && _currentResources.wood - toRemove.wood < 0)
            return false;

        //case where not enough stone to remove
        if (toRemove.stone > 0 && _currentResources.stone - toRemove.stone < 0)
            return false;

        //case where not enough food to remove
        if (toRemove.food > 0 && _currentResources.food - toRemove.food < 0)
            return false;

        //case where not enough workForce to remove
        if (toRemove.workForce > 0 && _currentResources.workForce - toRemove.workForce < 0)
            return false; // end of game ?

        //case where not enough motivation to remove
        if (toRemove.motivation > 0 && _currentResources.motivation - toRemove.motivation < 0)
            return false; // end of game ?

        //every resource level is high enough
        _currentResources -= toRemove;
        return true;
    }
}