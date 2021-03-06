﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
    public bool cinematic = false;

    public Transform Player;
    private Vector3 _playerLastPosition;

    //Audio
    private AudioSource _audioData;
    public AudioClip victorySound;
    public AudioClip defeatSound;

    public GameObject victoryFX;

    public string levelName;
    private float levelDuration = 0;
    public string nextSceneName;

    private void ResetState()
    {
        _hasStarted = true;
        _hasWin = false;
        _hasLost = false;
    }

    private void Start()
    {
        ResetState();
        Debug.Log("Is Game Won ? : " + IsGameWon());
        //if (levelName == "Map_Tuto1.04")
            //_playerLastPosition = Player.position;
        _audioData = GetComponent<AudioSource>();
        levelName = SceneManager.GetActiveScene().name;
        //Player.position = _playerLastPosition;
    }

    // ***** STATES OF THE GAME *****/
    public void GameStarted()
    {
        _hasStarted = true;
    }
    public void GameWon(int scoreCount = 0)
    {
        _hasWin = true;
        if (victorySound)
        {
            _audioData.clip = victorySound;
            _audioData.Play();
        }

        victoryFX.SetActive(true);

        // SAVE THE PLAYER's VICTORY
        SaveManager.Save(new LevelScore(levelName, scoreCount, levelDuration));

        StartCoroutine(ChangeScene(tuto ? 10 : 5));
    }
    public void GameLost()
    {
        _hasLost = true;
        if (defeatSound)
        {
            _audioData.clip = defeatSound;
            _audioData.Play();
        }

        StartCoroutine(ChangeScene());
    }
    public bool IsGameWon()
    {
        return _hasWin;
    }

    public bool IsGameLost()
    {
        return _hasLost;
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

    private void Update()
    {
        if (_hasStarted && !_isPaused)
        {
            levelDuration += Time.deltaTime;
        }

            // ERASE SAVE - Press K and P
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.E)) {
            SaveManager.Erase();
        }
    }

    // Change the scene after 5 sec
    private IEnumerator ChangeScene(float seconds = 5)
    {
                Debug.Log("Wait before change Scene to : " + nextSceneName);
        yield return new WaitForSeconds(seconds);

        if (_hasWin && nextSceneName != "")
        {
            Debug.Log("Change Scene");
            _playerLastPosition = Player.position;
            SceneManager.LoadScene(nextSceneName);
        }

        if (_hasLost)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
