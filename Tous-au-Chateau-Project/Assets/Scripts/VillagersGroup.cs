using UnityEngine;
using System.Collections.Generic;

public class VillagersGroup : MonoBehaviour
{
    public List<Villager> _villagers;

    
    public void AddVillagers()
    {
        Villager villager = new Villager();
        _villagers.Add(villager);
    }
    public void RemoveVillager()
    {
        _villagers.RemoveAt(_villagers.Count-1);
    }
    public void RemoveAllVillager()
    {
        foreach(Villager v in _villagers)
        {
            Destroy(v);
        }
        _villagers.Clear();
    }
    public int GetNumberOfVillagers()
    {
        return _villagers.Count;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
