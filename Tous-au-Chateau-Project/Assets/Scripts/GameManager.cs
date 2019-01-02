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
                GameObject go = new GameObject("GameManager");
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
    public void Pause()
    {
        _isPaused = true;
    }
    public void Resume()
    {
        _isPaused = false;
    }
    public void TogglePause()
    {
        _isPaused = !_isPaused;
    }
    public void PauseWorld()
    {
        _isWorldPaused = true;
    }
    public void ResumeWorld()
    {
        _isWorldPaused = false;
    }
    public void TogglePauseWorld()
    {
        _isWorldPaused = !_isWorldPaused;

        if (_isWorldPaused)
        {
            PauseVillagers();
        }
        else
        {
            ResumeVillagers();
        }
    }

    // Pause Effects
    private void PauseVillagers()
    {
        Debug.Log("Pause Villagers");
    }
    private void ResumeVillagers()
    {
        Debug.Log("Resume Villagers");
    }
}
