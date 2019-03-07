using System.Collections;
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

    //public VillagersGroup listOfVillagers;
    [Range(0.0f, 5.0f)]
    public float frequenceToReduceMotivation = 3.0f;

    [SerializeField]
    private ResourcesPack _currentResources;
    private bool _isInPause;

    void Start()
    {
        _currentResources = new ResourcesPack { motivation = 100, workForce = 10};
        _isInPause = false;
        InvokeRepeating("InGameMotivation", 0.0f, frequenceToReduceMotivation);
    }

    private void Update()
    {
        /*
        if(listOfVillagers != null)
        {
            _currentResources.workForce = listOfVillagers.GetNumberOfVillagers();
        }
        */
        _currentResources.workForce = VillagersManager.Instance.GetNumberOfVillagersAlive();
        if(_currentResources.workForce <= 0 && !GameManager.Instance.tuto)
        {
            GameManager.Instance.GameLost();
        }
    }

    private void InGameMotivation()
    {
        if (!_isInPause)
        {
            RemoveResources(new ResourcesPack { motivation = 1 });
            if(_currentResources.motivation <= 0 && !GameManager.Instance.tuto)
            {
                GameManager.Instance.GameLost();
            }
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

        //Special case for workforce => add in villagersgroup and not here

        if (_currentResources.motivation > 100)
            _currentResources.motivation = 100;
    }

    //REMOVE
    public bool RemoveResources(ResourcesPack toRemove)
    {
        bool hasEnoughResource = HasEnoughResources(toRemove);

        if (!hasEnoughResource)
        {
            return false;
        }

        //Special case for workforce => remove in villagersgroup and not here
        _currentResources -= toRemove;
        return true;
    }

    public bool HasEnoughResources(ResourcesPack toCheck)
    {
        //non valid values
        if (toCheck.wood < 0 || toCheck.stone < 0 || toCheck.food < 0 || toCheck.workForce < 0 || toCheck.motivation < 0)
            return false;

        //case where not enough wood
        if (toCheck.wood > 0 && _currentResources.wood - toCheck.wood < 0)
            return false;

        //case where not enough stone
        if (toCheck.stone > 0 && _currentResources.stone - toCheck.stone < 0)
            return false;

        //case where not enough food
        if (toCheck.food > 0 && _currentResources.food - toCheck.food < 0)
            return false;

        //case where not enough workForce
        if (toCheck.workForce > 0 && _currentResources.workForce - toCheck.workForce < 0)
            return false;

        //case where not enough motivation
        if (toCheck.motivation > 0 && _currentResources.motivation - toCheck.motivation < 0)
            return false;

        //every resource level is high enough
        return true;
    }
}
