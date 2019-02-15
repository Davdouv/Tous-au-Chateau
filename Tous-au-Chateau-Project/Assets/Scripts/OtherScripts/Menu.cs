using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject GameMaster;
    public Text GameSpeedDisplay;
    public Text GenerationCounter;
    public Text PlayerNumDisplay;
    public Slider GameSpeedSlider;
    public Slider PlayerNumSlider;
    static float gameSpeed = 1.0f;  //static so it doesn't change when level restarts
    public static int volcanoSmokeNum = 10;


    void Start()
    {
        UpdatePopNumberDisplay(volcanoSmokeNum);
        UpdateGameSpeedDisplay(gameSpeed);

        GameSpeedSlider.value = gameSpeed;
        PlayerNumSlider.value = volcanoSmokeNum;
    }

    // Update is called once per frame
    void Update () {
        UpdateGenerationText();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void ChangeGameSpeedValue(float newSpeed)
    {
        gameSpeed = newSpeed;
    }

    public void UpdateGameSpeedDisplay(float newSpeed)
    {
        GameSpeedDisplay.text = "x" + newSpeed.ToString();
    }

    public void UpdateGenerationText()
    {
        int generation = GameMaster.GetComponent<Population>().generation;
        GenerationCounter.text = "Generation: " + generation.ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangePopulationNumber(float n)
    {
        volcanoSmokeNum = (int)n;
    }

    public void UpdatePopNumberDisplay(float n)
    {
        PlayerNumDisplay.text = n.ToString();
    }
}
