using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// do not add update method when inheriting TriggerZone
public class InfectionSpreading : TriggerZone
{
    public List<string> _canInfect; // defined by user but need to be replicated to runtime-made copies
    private List<GameObject> _infectable;
    private CharacterStats _stats;
    public int _duration = 20; // malus duration
    public int _countdown= 5; // exposition countdown before close units get infected

    
    // Use this for initialization
    void Start () {
        _stats = GetComponent<CharacterStats>();
        Time.timeScale = 1;
        _infectable = new List<GameObject>();
        _stats.SetSpeed(_stats.GetSpeed() / 2);
        GetComponent<SphereCollider>().radius = 2.5f;
        targetTag.AddRange(_canInfect);
    }


    // On Detection, 
    public override void TriggerEnter(GameObject target)
    {
        Debug.Log("INFECTION TRIGGER ENTER : " + target.name);
        // 
        if (_canInfect.Contains(target.tag))
        {
            _infectable.Add(target);
            Contaminate(target);
        }
        
    }

    // On Detection Exit, 
    public override void TriggerExit(GameObject target)
    {
        Debug.Log("INFECTION TRIGGER EXIT : " + target.name);
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
        _infectable.Remove(target);
    }

    private void Contaminate(GameObject target)
    {
        StartCoroutine(CountDown(target));
        
    }
    IEnumerator CountDown(GameObject target)
    {
        int timeLeft = _countdown;
        while (IsInRange(target.transform.position) && timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            print(target.name+" :"+timeLeft);
        }
        if(timeLeft <= 0)
        {
            Infect(target);
        }

    }
    
}
