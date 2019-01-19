using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Building {

    private RessourcesPack _cost;
    private RessourcesPack _provision;
    private float _effectArea; //the radius of effect area
    private float _width;
    private float _height;
    private bool _isDraggable;

    public void crush()
    {

    }
    public bool canBuy()
    {
        return;
    }

    public bool ApplyEffect()
    {
        return;
    }

    public bool Drag()
    {
        return;
    }

}
