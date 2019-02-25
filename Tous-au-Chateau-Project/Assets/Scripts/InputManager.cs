using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class InputManager : MonoBehaviour {

    #region Singleton
    private static InputManager _instance;

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
    #endregion

    public VRTK_ControllerEvents leftControllerEvents;
    public VRTK_ControllerEvents rightControllerEvents;
    
    void Update()
    {
        // ***** KEYBOARD EVENTS *****/

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

    // ***** CONTROLLER EVENTS *****/

    private void OnEnable()
    {
        // Assign a method to buttons events
        leftControllerEvents.ButtonTwoReleased += ControllerEvents_ButtonTwoReleased;
        rightControllerEvents.ButtonTwoReleased += ControllerEvents_ButtonTwoReleased;
    }

    // PAUSE GAME
    private void ControllerEvents_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        GameManager.Instance.TogglePause();
    }
}
