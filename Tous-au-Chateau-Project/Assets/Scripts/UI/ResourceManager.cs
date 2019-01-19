using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : PauseScript
{
    public EndOfGameManager _EndOfGame;

    private ResourcesPack _currentResources;
    private bool _isInPause;

    void Start()
    {
        _currentResources = new ResourcesPack { motivation = 100, workForce = 10};
        _isInPause = false;
        InvokeRepeating("InGameMotivation", 0.0f, 3.0f);

        //test for resource pack operator override
        ResourcesPack test = new ResourcesPack { wood = 5, workForce = 3, food = 2 };
        _currentResources += test;

        Debug.Log(_currentResources);
    }

    void InGameMotivation()
    {
        if(!_isInPause)
            RemoveMotivation(1);
    }

    override public void Pause()
    {
        _isInPause = true;
    }

    override public void UnPause()
    {
        _isInPause = false;
    }

    /*GETTERS*/
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

    /* ADD */
    public void AddWood(int wood)
    {
        if (wood > 0)
        {
            _currentResources.wood += wood;
        }
    }

    public void AddStone(int stone)
    {
        if (stone > 0)
        {
            _currentResources.stone += stone;
        }
    }

    public void AddFood(int food)
    {
        if (food > 0)
        {
            _currentResources.food += food;
        }
    }

    public void AddWorkForce(int workFroce)
    {
        if (workFroce > 0)
        {
            _currentResources.workForce += workFroce;
        }
    }

    public void AddMotivation(int motivation)
    {
        if (motivation > 0)
        {
            if (_currentResources.motivation + motivation > 100)
            {
                _currentResources.motivation = 100;
            }
            else
            {
                _currentResources.motivation += motivation;
            }
        }
    }

    /* REMOVE */
    public bool RemoveWood(int wood)
    {
        if (wood > 0)
        {
            if(_currentResources.wood - wood < 0)
            {
                return false;
            }
            else
            {
                _currentResources.wood -= wood;
                return true;
            }
        }

        return false;
    }

    public bool RemoveStone(int stone)
    {
        if (stone > 0)
        {
            if (_currentResources.stone - stone < 0)
            {
                return false;
            }
            else
            {
                _currentResources.stone -= stone;
                return true;
            }
        }

        return false;
    }

    public bool RemoveFood(int food)
    {
        if (food > 0)
        {
            if (_currentResources.food - food < 0)
            {
                return false;
            }
            else
            {
                _currentResources.food -= food;
                return true;
            }
        }

        return false;
    }

    public bool RemoveWorkForce(int workForce)
    {
        if (workForce > 0)
        {
            if (_currentResources.workForce - workForce <= 0)
            {
                _currentResources.workForce = 0;
                _EndOfGame.LoseGame();
            }
            else
            {
                _currentResources.workForce -= workForce;
                return true;
            }
        }

        return false;
    }

    public void RemoveMotivation(int motivation)
    {
        if (motivation > 0)
        {
            if (_currentResources.motivation - motivation <= 0)
            {
                _currentResources.motivation = 0;
                _EndOfGame.LoseGame();
            }
            else
            {
                _currentResources.motivation -= motivation;
            }
        }
    }
}
