using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesPack 
{

    public int wood = 0;
    public int stone = 0;
    public int food = 0;
    public int workForce = 0;
    public int motivation = 0;

    public ResourcesPack(int wood = 0, int stone = 0, int food = 0, int workForce = 0, int motivation = 0)
    {
        this.wood = wood;
        this.stone = stone;
        this.food = food;
        this.workForce = workForce;
        this.motivation = motivation;
    }

    public static ResourcesPack operator+ (ResourcesPack resourcePack1, ResourcesPack resourcePack2)
    {
        ResourcesPack rsResult = new ResourcesPack
        {
            wood = resourcePack1.wood + resourcePack2.wood,
            stone = resourcePack1.stone + resourcePack2.stone,
            food = resourcePack1.food + resourcePack2.food,
            workForce = resourcePack1.workForce + resourcePack2.workForce,
            motivation = resourcePack1.motivation + resourcePack2.motivation
        };

        return rsResult;
    }

    public static ResourcesPack operator- (ResourcesPack resourcePack1, ResourcesPack resourcePack2)
    {
        ResourcesPack rsResult = new ResourcesPack
        {
            wood = resourcePack1.wood - resourcePack2.wood,
            stone = resourcePack1.stone - resourcePack2.stone,
            food = resourcePack1.food - resourcePack2.food,
            workForce = resourcePack1.workForce - resourcePack2.workForce,
            motivation = resourcePack1.motivation - resourcePack2.motivation
        };

        return rsResult;
    }

    public override string ToString()
    {
        return "Resources Pack : wood = " + wood + "; stone = " + stone + "; food = " + food + "; workForce = " + workForce + "; motivation = " + motivation + ";";
    }
}
