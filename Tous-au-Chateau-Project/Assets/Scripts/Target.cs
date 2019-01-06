using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target {

    private GameObject _targetObject;
    private bool _isTriggered;

    public Target(GameObject target)
    {
        _targetObject = target;
        _isTriggered = false;
    }

    public void Trigger()
    {
        _isTriggered = true;
    }

    public bool IsTriggered()
    {
        return _isTriggered;
    }

    public GameObject GetObject()
    {
        return _targetObject;
    }
}
