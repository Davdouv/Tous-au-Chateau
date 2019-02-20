using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { RIGHT, LEFT };

public class DirectionalSign : Building
{
    public Direction direction;

    public override bool ApplyEffect()
    {
        return true;
    }
}
