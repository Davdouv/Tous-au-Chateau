using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SpawnVillagers : MonoBehaviour {

    public Transform villagerSpawnTransform;
    public GameObject prefabVillagersGroup;

    /*
    private VillagersGroup _currentGroup = null;

    
    public override void TriggerEnter(GameObject target)
    {
        Debug.Log("Detected");
        if (target.GetComponent<Villager>().GetGroup() != _currentGroup)
        {
            _currentGroup = target.GetComponent<Villager>().GetGroup();
            SpawnGroup();
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Detected");
        if (collider.GetComponent<Villager>().GetGroup() != _currentGroup)
        {
            _currentGroup = collider.GetComponent<Villager>().GetGroup();
            SpawnGroup();
        }
    }
    */

    public void SpawnGroup()
    {
        Instantiate(prefabVillagersGroup, villagerSpawnTransform.position, villagerSpawnTransform.rotation);
    }
}
