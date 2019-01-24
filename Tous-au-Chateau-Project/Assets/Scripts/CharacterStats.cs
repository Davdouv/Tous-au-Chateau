using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public bool isAlive;
    public int life;
    public float speed = 1.50f;
    public float strength;
    
    public CharacterStats():this(true, 100, 1.50f, 0) { }

    public CharacterStats(bool live, int vie, float vitesse, float force)
    {
        isAlive = live;
        life = vie;
        speed = vitesse;
        strength = force;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
