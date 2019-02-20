using UnityEngine;
using System.Collections.Generic;

public class VillagersGroup : MonoBehaviour
{
    //private static VillagersGroup _instance = null;

    private List<Villager> _villagers = new List<Villager>();
    /*private VillagersGroup(){}
    public static VillagersGroup Instance
    {
        get{
            if (!_instance)
            {
                GameObject go = new GameObject("VillagersGroup");
                _instance = go.AddComponent<VillagersGroup>();
                
            }
            return _instance;
        }
        
    }*/

    public void AddVillagers()
    {
        Villager villager = new Villager();
        _villagers.Add(villager);
    }
    public void RemoveVillager()
    {
        _villagers.RemoveAt(_villagers.Count-1);
    }
    public void AddVillagers(Villager villager)
    {
        _villagers.Add(villager);
    }
    public void RemoveVillager(Villager villager)
    {
        _villagers.Remove(villager);
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

    public int GetNumberOfVillagersAlive()
    {
        int count = 0;
        _villagers.ForEach(villager => { if (villager.GetStats().IsAlive()) ++count; });
        return count;
    }

    public bool IsDeathCausedBy(DeathReason deathReason)
    {
        foreach (Villager villager in _villagers)
        {
            if (villager.GetStats().GetDeathReason() == deathReason)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
