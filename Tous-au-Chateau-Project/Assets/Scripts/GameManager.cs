using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    private static GameManager _instance;
    
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

    #endregion

    private static bool _hasStarted;
    private static bool _hasWin;
    private static bool _hasLost;
    private static bool _isPaused;
    private static bool _isWorldPaused;

    public GameObject pauseMenu;
    public bool tuto = true;

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
    public bool IsGameWon()
    {
        return _hasWin;
    }

    // PAUSE GAME
    public bool IsPaused()
    {
        return _isPaused;
    }
    public void Pause()
    {
        PauseWorld();
        _isPaused = true;
        pauseMenu.SetActive(true);
        Debug.Log("Pause Game");
    }
    public void Resume()
    {
        _isPaused = false;
        pauseMenu.SetActive(false);
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
    public bool IsWorldPaused()
    {
        return _isWorldPaused;
    }
    public void PauseWorld()
    {
        if (!_isPaused)
        {
            _isWorldPaused = true;
            Debug.Log("Pause World");
            //Time.timeScale = 0;
            // If we set timeScale to 0, then we can't move the controllers
        }
    }
    public void ResumeWorld()
    {
        if (!_isPaused)
        {
            _isWorldPaused = false;
            Debug.Log("Resume World");
            //Time.timeScale = 1;
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
