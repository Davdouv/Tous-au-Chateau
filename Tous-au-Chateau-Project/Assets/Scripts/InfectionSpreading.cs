using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionSpreading : TriggerZone
{
    public List<string> _canInfect;
    private List<GameObject> _infectable;
    private CharacterStats _stats;
    /* Hérité de TriggerZone
    
    public List<string> targetTag = new List<string>();
    public float distanceDetection = 5.0f;
    
    protected bool _isInContact;
    protected List<Target> _targetList = new List<Target>();


    public bool IsInContact()
    public bool IsInRange(Vector3 position)

    public void AddTarget(GameObject target)
    protected void RemoveTarget(GameObject target)

    public virtual void TriggerEnter(GameObject target) { }
    public virtual void TriggerExit(GameObject target) { }
    public virtual void CollisionEnter(Collision collision) { }
    public virtual void CollisionExit(Collision collision) { }
    
         
    */
    // Use this for initialization
    void Start () {
        _stats = GetComponent<CharacterStats>();
	}


    // On Detection, 
    public override void TriggerEnter(GameObject target)
    {
        Debug.Log("TRIGGER ENTER : " + target.name);
        // 
        if (_canInfect.Contains(target.tag))
        {
            _infectable.Add(target);
        }
        
    }

    // On Detection Exit, 
    public override void TriggerExit(GameObject target)
    {
        //Debug.Log("TRIGGER EXIT : " + target.name);
        // 
        if (_canInfect.Contains(target.tag))
        {
            // Remove it from the list
            _infectable.Remove(target.gameObject);
            
        }
    }

    // On Collision, 
    public override void CollisionEnter(Collision collision)
    {
        //Debug.Log("COLLISION ENTER : " + collision.gameObject.name);
        // Check if it's the target we are aiming
        if (_infectable.Contains(collision.gameObject))
        {
            Infect(collision.gameObject);
            _infectable.Remove(collision.gameObject);
        }
    }

    // On Collision Exit, 
    public override void CollisionExit(Collision collision)
    {
        //Debug.Log("COLLISION EXIT : " + collision.gameObject.name);
        
    }

    private void Infect(GameObject target)
    {
        target.AddComponent<InfectionSpreading>();
    }


    // Update is called once per frame
    void Update () {
		//make countdown class
	}
}
