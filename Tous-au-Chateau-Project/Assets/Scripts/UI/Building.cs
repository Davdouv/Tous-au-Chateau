using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {

    protected ResourcesPack _cost;
    public ResourceManager _resourceManager;
    private float _effectArea; //the radius of effect area
    private float _width;
    private float _height;
    private bool _isDraggable;

    public virtual void crush()
    {

    }
    public virtual bool canBuy()
    {
        return _resourceManager.RemoveWood(_cost.wood) && _resourceManager.RemoveWood(_cost.stone);
    }

    public virtual bool ApplyEffect()
    {
        return false;
    }

    public virtual bool Drag()
    {
        return false;
    }

}
