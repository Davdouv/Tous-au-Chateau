using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionSpreading : TriggerZone
{
    public List<string> _canInfect;
    private List<GameObject> _infectable;
    private CharacterStats _stats;
    public int _duration = 20; // malus duration
    public int _countdown= 20; // exposition countdown before close units get infected

    
    // Use this for initialization
    void Start () {
        _stats = GetComponent<CharacterStats>();
        Time.timeScale = 1;
        _stats.SetSpeed(_stats.GetSpeed() / 2);
	}


    // On Detection, 
    public override void TriggerEnter(GameObject target)
    {
        Debug.Log("TRIGGER ENTER : " + target.name);
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
        _infectable.Remove(target);
    }

    private void Contaminate(GameObject target)
    {
        StartCoroutine(CountDown(target));
        
    }
    IEnumerator CountDown(GameObject target)
    {
        int timeLeft = _countdown;
        while (IsInRange(target.transform.position))
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
        if(timeLeft <= 0)
        {
            Infect(target);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
