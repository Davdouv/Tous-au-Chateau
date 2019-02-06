using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    protected ResourcesPack _cost;
    private float _effectArea; //the radius of effect area
    private float _width;
    private float _height;
    private bool _isDraggable;

    private void Start()
    {
        _cost = new ResourcesPack();
    }

    public virtual void Crush()
    {
        // if(this.gameObject.CompareTag("crushable"))
        //apply function VR 

    }

    public bool CanBuy()
    {
        return ResourceManager.Instance.RemoveResources(_cost);
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
