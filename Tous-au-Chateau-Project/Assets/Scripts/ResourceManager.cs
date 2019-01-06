using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : PauseScript
{
    public EndOfGameManager _EndOfGame;

    private int _woodNb = 15;
    private int _stoneNb = 10;
    private int _foodNb = 0;
    private int _villagersNb = 30;
    private float _motivation = 100; //% => value from 0 to 100
    private bool _isInPause;

    void Start()
    {
        _isInPause = false;
        InvokeRepeating("InGameMotivation", 0.0f, 3.0f);
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
        return _woodNb;
    }

    public int GetStone()
    {
        return _stoneNb;
    }

    public int GetFood()
    {
        return _foodNb;
    }

    public int GetVillagers()
    {
        return _villagersNb;
    }

    public float GetMotivation()
    {
        return _motivation;
    }

    /*SETTERS*/
    public void SetWood(int wood)
    {
        if (wood >= 0)
        {
            _woodNb = wood;
        }
    }

    public void SetStone(int stone)
    {
        if(stone >= 0)
        {
            _stoneNb = stone;
        }
    }

    public void SetFood(int food)
    {
        if(food >= 0)
        {
            _foodNb = food;
        }
    }

    public void SetVillagers(int villagers)
    {
        if(villagers >= 0)
        {
            _villagersNb = villagers;
        }
    }

    public void SetMotivation(float motivation)
    {
        //% => value from 0 to 100
        if (motivation >= 0 && motivation <= 100)
        {
            _motivation = motivation;
        }
    }

    /* ADD */
    public void AddWood(int wood)
    {
        if (wood > 0)
        {
            _woodNb += wood;
        }
    }

    public void AddStone(int stone)
    {
        if (stone > 0)
        {
            _stoneNb += stone;
        }
    }

    public void AddFood(int food)
    {
        if (food > 0)
        {
            _foodNb += food;
        }
    }

    public void AddVillagers(int villagers)
    {
        if (villagers > 0)
        {
            _villagersNb += villagers;
        }
    }

    public void AddMotivation(int motivation)
    {
        if (motivation > 0)
        {
            if (_motivation + motivation > 100)
            {
                _motivation = 100;
            }
            else
            {
                _motivation += motivation;
            }
        }
    }

    /* REMOVE */
    public bool RemoveWood(int wood)
    {
        if (wood > 0)
        {
            if(_woodNb - wood < 0)
            {
                return false;
            }
            else
            {
                _woodNb -= wood;
                return true;
            }
        }

        return false;
    }

    public bool RemoveStone(int stone)
    {
        if (stone > 0)
        {
            if (_stoneNb - stone < 0)
            {
                return false;
            }
            else
            {
                _stoneNb -= stone;
                return true;
            }
        }

        return false;
    }

    public bool RemoveFood(int food)
    {
        if (food > 0)
        {
            if (_foodNb - food < 0)
            {
                return false;
            }
            else
            {
                _foodNb -= food;
                return true;
            }
        }

        return false;
    }

    public bool RemoveVillagers(int villagers)
    {
        if (villagers > 0)
        {
            if (_villagersNb - villagers <= 0)
            {
                _villagersNb = 0;
                _EndOfGame.LoseGame();
            }
            else
            {
                _villagersNb -= villagers;
                return true;
            }
        }

        return false;
    }

    public void RemoveMotivation(int motivation)
    {
        if (motivation > 0)
        {
            if (_motivation - motivation <= 0)
            {
                _motivation = 0;
                _EndOfGame.LoseGame();
            }
            else
            {
                _motivation -= motivation;
            }
        }
    }
}
