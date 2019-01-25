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

    public static ResourcesPack operator+ (ResourcesPack resourcePack1, ResourcesPack resourcePack2)
    {
        ResourcesPack rsResult = new ResourcesPack();

        rsResult.wood = resourcePack1.wood + resourcePack2.wood;
        rsResult.stone = resourcePack1.stone + resourcePack2.stone;
        rsResult.food = resourcePack1.food + resourcePack2.food;
        rsResult.workForce = resourcePack1.workForce + resourcePack2.workForce;
        rsResult.motivation = resourcePack1.motivation + resourcePack2.motivation;

        return rsResult;
    }

    public static ResourcesPack operator- (ResourcesPack resourcePack1, ResourcesPack resourcePack2)
    {
        ResourcesPack rsResult = new ResourcesPack();

        rsResult.wood = resourcePack1.wood - resourcePack2.wood;
        rsResult.stone = resourcePack1.stone - resourcePack2.stone;
        rsResult.food = resourcePack1.food - resourcePack2.food;
        rsResult.workForce = resourcePack1.workForce - resourcePack2.workForce;
        rsResult.motivation = resourcePack1.motivation - resourcePack2.motivation;

        return rsResult;
    }

    public override string ToString()
    {
        return "Resources Pack : wood = " + wood + "; stone = " + stone + "; food = " + food + "; workForce = " + workForce + "; motivation = " + motivation + ";";
    }
}
