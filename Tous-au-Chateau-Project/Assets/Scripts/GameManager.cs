using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    private bool _hasStarted;
    private bool _hasWin;
    private bool _hasLost;
    private bool _isPaused;
    private bool _isWorldPaused;

    // ***** SINGLETON *****/
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("_GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }

    // ***** STATES OF THE GAME *****/
    public void GameStarted()
    {
        _hasStarted = true;
    }
    public void GameWon()
    {
        _hasWin = true;
    }
    public void GameLost()
    {
        _hasLost = true;
    }

    // PAUSE GAME
    public void Pause()
    {
        PauseWorld();
        _isPaused = true;               
        Debug.Log("Pause Game");
    }
    public void Resume()
    {
        _isPaused = false;
        ResumeWorld();
        Debug.Log("Resume Game");
    }
    public void TogglePause()
    {
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    // PAUSE WORLD
    public void PauseWorld()
    {
        if (!_isPaused)
        {
            _isWorldPaused = true;
            Time.timeScale = 0;
            Debug.Log("Pause World");
        }
    }
    public void ResumeWorld()
    {
        if (!_isPaused)
        {
            _isWorldPaused = false;
            Time.timeScale = 1;
            Debug.Log("Resume World");
        }
    }
    public void TogglePauseWorld()
    {
        if (_isWorldPaused)
        {
            ResumeWorld();
        }
        else
        {
            PauseWorld();
        }
    }

    // RESTART
    public void Restart()
    {
        _hasStarted = false;
        _hasWin = false;
        _hasLost = false;
        _isPaused = false;
        _isWorldPaused = false;
    }
}
