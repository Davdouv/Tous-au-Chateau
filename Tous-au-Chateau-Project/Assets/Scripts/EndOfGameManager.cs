using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameManager : MonoBehaviour {

    public UIManager uiManager;
    public PauseScript[] pauseObjects;

    public void LoseGame()
    {
        uiManager.DisplayGameOverPanel();
        for(int i = 0; i < pauseObjects.Length; ++i)
        {
            pauseObjects[i].Pause();
        }
    }

    public void WinGame()
    {
        //uiManager.DisplayVictoryPanel();
        for (int i = 0; i < pauseObjects.Length; ++i)
        {
            pauseObjects[i].Pause();
        }
    }
}
