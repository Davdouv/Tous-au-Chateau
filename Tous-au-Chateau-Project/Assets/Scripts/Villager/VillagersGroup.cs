using UnityEngine;
using System.Collections.Generic;

public class VillagersGroup : MonoBehaviour
{
    private List<Villager> _villagers = new List<Villager>();

    // Register the group into the villagers Manager
    private void Start()
    {
        VillagersManager.Instance.AddGroup(this);
    }

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
