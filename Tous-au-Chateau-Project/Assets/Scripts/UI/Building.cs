using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    protected ResourcesPack _cost;
    public ResourceManager _resourceManager;
    private float _effectArea; //the radius of effect area
    private float _width;
    private float _height;
    private bool _isDraggable;

    public virtual void Crush()
    {

    }
    public virtual bool CanBuy()
    {
        return _resourceManager.Buy(_cost);
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
