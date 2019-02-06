using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : TriggerZone {

    public GameObject castleDestination;
    public GameObject escapeDestination;
    public GameObject evilLord;
    public List<GameObject> villagers = new List<GameObject>();
    public float speed;
    private float _step;
    private bool _isNearCastle = false;
    private bool _isEscaping = false;
    private bool _hasEscaped = false; // use this to know if the lord escaped
    
    public override void TriggerEnter(GameObject villager)
    {
        Debug.Log("VICTORY ! A villager is in the castle !");
        villagers.Add(villager);
        _isNearCastle = true; // a villager reach the castle
        _isEscaping = true; // evil lord escapes
    }

    protected override void Update()
    {
        base.Update();

        _step = speed * Time.deltaTime;

        // if a villager reachs the castle
        if (_isNearCastle)
        {
            foreach (GameObject villager in villagers)
            {
                villager.transform.position = Vector3.MoveTowards(villager.transform.position, castleDestination.transform.position, _step); // villagers walk inside the castle
            }
        }

        if (_isEscaping)
        {
            evilLord.transform.position = Vector3.MoveTowards(evilLord.transform.position, escapeDestination.transform.position, _step); // evil lord escapes
            evilLord.transform.LookAt(escapeDestination.transform);
        }

        if (Vector3.Distance(evilLord.transform.position, escapeDestination.transform.position) < 2)
        {
            _hasEscaped = true;
            Debug.Log("The evil lord has escaped :'(");
            _isEscaping = false;
        }
    }
}
