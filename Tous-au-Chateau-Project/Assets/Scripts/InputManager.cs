﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static InputManager _instance;

    public Camera mainCam;

    // ***** SINGLETON *****/
    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("_InputManager");
                go.AddComponent<InputManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        // PAUSE WORLD
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.TogglePauseWorld();
        }

        // PAUSE GAME (Pause Menu)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.TogglePause();
        }
    }
}