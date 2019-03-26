using System.Collections; using System.Collections.Generic; using UnityEngine;  public enum BuildingType { Basics, DirectionalPanel };  public class Building : MonoBehaviour {      public string _name;     public BuildingType buildingType;     public GameObject prefab;
    public GameObject prefabTransparent;     private AudioSource _audioData;     public AudioClip purchasedSound;      public GameObject leftCorner;
    public GameObject rightCorner;      [SerializeField]     protected ResourcesPack _cost;          private float _width;     private float _height;     private bool _isDraggable;      private bool _hasLanded;      void Start()
    {
        _audioData = GetComponent<AudioSource>();
        _audioData.clip = purchasedSound;
    }      public virtual void Crush()     {         // if(this.gameObject.CompareTag("crushable"))         //apply function VR      }      public bool CanBuy()     {         bool hasEnoughResources = ResourceManager.Instance.RemoveResources(_cost);          if (!hasEnoughResources)
        {
            BuildingsTypeGroup.Instance.PlaySound();
        }
        else
        {
            _audioData.Play();
        }          return hasEnoughResources;     }      public virtual bool Drag()     {         return false;     }      public string GetCostString()     {         string costStr = "";          if (_cost == null)             return costStr;          if (_cost.wood > 0) { costStr += _cost.wood + " <sprite=0>"; }         if (_cost.stone > 0) { costStr += _cost.stone + " <sprite=1>"; }         if (_cost.food > 0) { costStr += _cost.food + " <sprite=2>"; }         if (_cost.workForce > 0) { costStr += _cost.workForce + " <sprite=3>"; }         if (_cost.motivation > 0) { costStr += _cost.motivation + " <sprite=4>"; }          return costStr;     }      public virtual void SetHasLanded()
    {
        if (!_hasLanded)
        {
            _audioData.Play();
            _hasLanded = true;
        }        
    }          public bool HasLanded()
    {
        return _hasLanded;
    }      public ResourcesPack getCost()
    {
        return _cost;
    } } 