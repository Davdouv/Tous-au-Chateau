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
    private bool _isEscaping = false;
    private bool _hasEscaped = false;
    
    public override void TriggerEnter(GameObject villager)
    {
        Debug.Log("VICTORY ! A villager is in the castle !");
        villagers.Add(villager);
        // evil lord escapes
        _isEscaping = true;
    }

    protected override void Update()
    {
        base.Update();

        _step = speed * Time.deltaTime;

        // if a villager reachs the castle
        if (_isEscaping)
        {
            // evil lord escapes
            evilLord.transform.position = Vector3.MoveTowards(evilLord.transform.position, escapeDestination.transform.position, _step);

            // villagers walk inside the castle
            foreach (GameObject villager in villagers)
            {
                villager.transform.position = Vector3.MoveTowards(villager.transform.position, castleDestination.transform.position, _step);
            }
        }

        if (Vector3.Distance(evilLord.transform.position, escapeDestination.transform.position) < 2)
        {
            _hasEscaped = true;
            Debug.Log("The evil lord has escaped :'(");
        }
    }
}
