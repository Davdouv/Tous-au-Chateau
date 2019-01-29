using UnityEngine;
using System.Collections;

public class CharacterStats
{
    public bool _isAlive;
    public int _life;
    public float _speed = 1.50f;
    public float _strength;
    
    public CharacterStats():this(true, 100, 1.50f, 0) { }

    public CharacterStats(bool live, int vie, float vitesse, float force)
    {
        _isAlive = live;
        _life = vie;
        _speed = vitesse;
        _strength = force;
    }
    // Setters
    public void SetIsAlive(bool live) { _isAlive = live; }
    public void SetLife(int vie) { _life = vie; }
    public void SetSpeed(float vitesse) { _speed = vitesse; }
    public void SetStrength(float force) { _strength = force; }
    // Getters
    public bool GetIsAlive() { return _isAlive; }
    public int GetLife() { return _life; }
    public float GetSpeed() { return _speed; }
    public float GetStrength() { return _strength; }
    
}
