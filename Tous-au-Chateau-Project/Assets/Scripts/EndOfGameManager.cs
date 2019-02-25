using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameManager : MonoBehaviour {

    /******************************
     * OLD SCRIPT NOT USED ANYMORE
     *****************************/

    public UIManager _UiManager;
    public PauseScript[] pauseObjects;

    public void LoseGame()
    {
        //_UiManager.DisplayGameOverPanel();
        for(int i = 0; i < pauseObjects.Length; ++i)
        {
            pauseObjects[i].Pause();
        }
    }
}
