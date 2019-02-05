using System.Collections; using System.Collections.Generic; using UnityEngine;  public class Building : MonoBehaviour {      public string _name;     [Range(0, 500)]     public int woodCost;     [Range(0, 500)]     public int stoneCost;     [Range(0, 500)]     public int foodCost;     [Range(0, 500)]     public int workForceCost;     [Range(0, 500)]     public int motivationCost;      protected ResourcesPack _cost;          private float _effectArea; //the radius of effect area     private float _width;     private float _height;     private bool _isDraggable;        private void Start()     {         _cost = new ResourcesPack { stone = stoneCost, wood = woodCost, food = foodCost, workForce = workForceCost, motivation = motivationCost };     }      public virtual void Crush()     {         // if(this.gameObject.CompareTag("crushable"))         //apply function VR       }      public bool CanBuy()     {         return ResourceManager.Instance.RemoveResources(_cost);     }      public virtual bool ApplyEffect()     {         return false;     }      public virtual bool Drag()     {         return false;     }      public string GetCostString()
    {
        string costStr = "";

        if (_cost.wood > 0) { costStr += "w : " + _cost.wood; }
        if (_cost.stone > 0) { costStr += " s : " + _cost.stone; }
        if (_cost.food > 0) { costStr += " f : " + _cost.food; }
        if (_cost.workForce > 0) { costStr += " wf : " + _cost.workForce; }
        if (_cost.motivation > 0) { costStr += " m : " + _cost.motivation; }

        return costStr;
    }  } 