using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapPhysicObject : MonoBehaviour {

    protected bool _canBeCrushed;

    public virtual void Crush() { }
}
