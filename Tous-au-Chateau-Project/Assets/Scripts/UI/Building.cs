using System.Collections; using System.Collections.Generic; using UnityEngine;  public enum BuildingType { Basics, DirectionalPanel };  public class Building : MonoBehaviour {      public string _name;     public BuildingType buildingType;     public GameObject prefab;      [SerializeField]     protected ResourcesPack _cost;          private float _width;     private float _height;     private bool _isDraggable;      private bool _hasLanded;      public virtual void Crush()     {         // if(this.gameObject.CompareTag("crushable"))         //apply function VR      }      public bool CanBuy()     {         return ResourceManager.Instance.RemoveResources(_cost);     }      public virtual bool Drag()     {         return false;     }      public string GetCostString()     {         string costStr = "";          if (_cost == null)             return costStr;          if (_cost.wood > 0) { costStr += "w : " + _cost.wood; }         if (_cost.stone > 0) { costStr += " s : " + _cost.stone; }         if (_cost.food > 0) { costStr += " f : " + _cost.food; }         if (_cost.workForce > 0) { costStr += " wf : " + _cost.workForce; }         if (_cost.motivation > 0) { costStr += " m : " + _cost.motivation; }          return costStr;     }      public void SetHasLanded()
    {
        if (!_hasLanded)
        {
            _hasLanded = true;
        }        
    }          public bool HasLanded()
    {
        return _hasLanded;
    }      public ResourcesPack getCost()
    {
        return _cost;
    } } 