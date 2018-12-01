using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    private int woodNb = 0;
    private int stoneNb = 0;
    private int foodNb = 0;
    private int villagersNb = 30;
    private float motivation = 100; //% => value from 0 to 100

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
        return villagersNb;
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

    public void SetVillagers(int villagers)
    {
        if(villagers >= 0)
        {
            villagersNb = villagers;
        }
    }

    public void SetMotivation(float newMotivation)
    {
        //% => value from 0 to 100
        if (newMotivation >= 0 && newMotivation <= 100)
        {
            motivation = newMotivation;
        }
    }
}
