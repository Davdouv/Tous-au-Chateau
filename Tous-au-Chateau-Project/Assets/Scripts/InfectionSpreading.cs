using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// do not add update method when inheriting TriggerZone
public class InfectionSpreading : TriggerZone
{
    private List<string> _canInfect; 
    private List<GameObject> _infectable;
    public int _duration = 20; // malus duration
    public int _countdown= 5; // exposition countdown before close units get infected

    
    // Use this for initialization
    void Start () {
        CharacterStats _stats = GetComponent<Villager>().GetStats();
        _canInfect = new List<string> { tag };
        Time.timeScale = 1;
        _infectable = new List<GameObject>();
        _stats.speed = _stats.speed / 2;
        distanceDetection = 2.5f;
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
        //Debug.Log("INFECTION TRIGGER EXIT : " + target.name);
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
        Debug.Log("COLLISION ENTER : " + collision.gameObject.name);
        // Check if it's the target we are aiming
        if (_infectable.Contains(collision.gameObject))
        {
            Debug.Log("HE CAN BE INFECTED : " + collision.gameObject.name);
            Infect(collision.gameObject);
        }
    }
    

    private void Infect(GameObject target)
    {
        Component alreadyinfected = target.GetComponent<InfectionSpreading>();
        if (!alreadyinfected)
        {
            //target.GetComponent<Villager>().GetInfected();  villagers only
            target.AddComponent<InfectionSpreading>(); // need a mother class that implement GetInfected() to generalize
            _infectable.Remove(target);
        }
        
    }

    private void Contaminate(GameObject target)
    {
        StartCoroutine(CountDown(target));
        
    }
    IEnumerator CountDown(GameObject target)
    {
        int timeLeft = _countdown;
        while (IsInRange(target.transform.position) && timeLeft > 0 && !target.GetComponent<InfectionSpreading>())
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
