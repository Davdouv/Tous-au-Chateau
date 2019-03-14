using System.Collections;
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

    //Audio
    private AudioSource _audioData;
    public AudioClip victorySound;
    public AudioClip defeatSound;

    public GameObject victoryFX;

    public string levelName;
    private float levelDuration = 0;
    public string nextSceneName;

    private void Start()
    {
        _audioData = GetComponent<AudioSource>();
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

        StartCoroutine(ChangeScene());
    }
    public void GameLost()
    {
        _hasLost = true;
        if (defeatSound)
        {
            _audioData.clip = defeatSound;
            _audioData.Play();
        }
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
    }

    // Change the scene after 5 sec
    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(nextSceneName);
    }

}
