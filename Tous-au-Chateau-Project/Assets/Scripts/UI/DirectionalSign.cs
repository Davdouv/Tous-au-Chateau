using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { RIGHT, LEFT, BACKWARD };

public class DirectionalSign : Building
{
    public Direction direction;

    void Start()
    {
        // wood = 20, stone = 0, food = 0
        _cost = new ResourcesPack(20);
    }

    public override bool ApplyEffect()
    {
        return true;
    }
}
