using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : PauseScript
{
    public EndOfGameManager endOfGame;

    private int woodNb = 15;
    private int stoneNb = 10;
    private int foodNb = 0;
    private VillagersGroup _villagers;
    private float motivation = 100; //% => value from 0 to 100
    private bool isInPause;

    void Start()
    {
        isInPause = false;
        InvokeRepeating("InGameMotivation", 0.0f, 3.0f);
    }

    void InGameMotivation()
    {
        if(!isInPause)
            RemoveMotivation(1);
    }

    override public void Pause()
    {
        isInPause = true;
    }

    override public void UnPause()
    {
        isInPause = false;
    }

    /*GETTERS*/
    public int GetWood()
    {
        return woodNb;
    }

    public int GetStone()
    {
        return stoneNb;
    }

    public int GetFood()
    {
        return foodNb;
    }

    public int GetVillagers()
    {
        return _villagers.GetNumberOfVillagers();
    }

    public float GetMotivation()
    {
        return motivation;
    }

    /*SETTERS*/
    public void SetWood(int wood)
    {
        if (wood >= 0)
        {
            woodNb = wood;
        }
    }

    public void SetStone(int stone)
    {
        if(stone >= 0)
        {
            stoneNb = stone;
        }
    }

    public void SetFood(int food)
    {
        if(food >= 0)
        {
            foodNb = food;
        }
    }
    /*
    public void SetVillagers(int villagers)
    {
        if(villagers >= 0)
        {
            villagersNb = villagers;
        }
    }
    */
    public void SetMotivation(float newMotivation)
    {
        //% => value from 0 to 100
        if (newMotivation >= 0 && newMotivation <= 100)
        {
            motivation = newMotivation;
        }
    }

    /* ADD */
    public void AddWood(int wood)
    {
        if (wood > 0)
        {
            woodNb += wood;
        }
    }

    public void AddStone(int stone)
    {
        if (stone > 0)
        {
            stoneNb += stone;
        }
    }

    public void AddFood(int food)
    {
        if (food > 0)
        {
            foodNb += food;
        }
    }
    
    public void AddVillagers(int villagers)
    {
        if (villagers > 0)
        {
           _villagers.AddVillagers();
        }
    }
    
    public void AddMotivation(int newMotivation)
    {
        if (newMotivation > 0)
        {
            if (motivation + newMotivation > 100)
            {
                motivation = 100;
            }
            else
            {
                motivation += newMotivation;
            }
        }
    }

    /* REMOVE */
    public bool RemoveWood(int wood)
    {
        if (wood > 0)
        {
            if(woodNb - wood < 0)
            {
                return false;
            }
            else
            {
                woodNb -= wood;
                return true;
            }
        }

        return false;
    }

    public bool RemoveStone(int stone)
    {
        if (stone > 0)
        {
            if (stoneNb - stone < 0)
            {
                return false;
            }
            else
            {
                stoneNb -= stone;
                return true;
            }
        }

        return false;
    }

    public bool RemoveFood(int food)
    {
        if (food > 0)
        {
            if (foodNb - food < 0)
            {
                return false;
            }
            else
            {
                foodNb -= food;
                return true;
            }
        }

        return false;
    }
    
    public bool RemoveVillagers(int villagers)
    {
        if (villagers > 0)
        {
            if (_villagers.GetNumberOfVillagers() - villagers <= 0)
            {
                _villagers.RemoveAllVillager();
                endOfGame.LoseGame();
            }
            else
            {
                for(int i =0; i< villagers; i++)
                {
                    _villagers.RemoveVillager();
                }
                return true;
            }
        }

        return false;
    }
    
    public void RemoveMotivation(int newMotivation)
    {
        if (newMotivation > 0)
        {
            if (motivation - newMotivation <= 0)
            {
                motivation = 0;
                endOfGame.LoseGame();
            }
            else
            {
                motivation -= newMotivation;
            }
        }
    }
}
