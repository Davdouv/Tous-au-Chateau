using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : PauseScript
{
    public Text woodTxt;
    public Text stoneTxt;
    public Text foodTxt;
    public Text villagersTxt;
    public Slider motivation;

    public GameObject GameOverPanel;
    public Text gameOverVillagersText;
    public ResourceManager ResourceManager;

    private void Update()
    {
        woodTxt.text = "" + ResourceManager.GetWood();
        stoneTxt.text = "" + ResourceManager.GetStone();
        foodTxt.text = "" + ResourceManager.GetFood();
        villagersTxt.text = "" + ResourceManager.GetWorkForce();
        motivation.value = ResourceManager.GetMotivation();
    }

    public void DisplayGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        gameOverVillagersText.text = "Remaining Villagers : " + ResourceManager.GetWorkForce();
    }

    override public void Pause()
    {
        /*Button[] interactables = constructionPanel.GetComponentsInChildren<Button>();
        for(int i = 0; i < interactables.Length; ++i)
        {
            interactables[i].enabled = false;
        }*/
    }

    override public void UnPause()
    {
        /*Button[] interactables = constructionPanel.GetComponentsInChildren<Button>();

        for (int i = 0; i < interactables.Length; ++i)
        {
            interactables[i].enabled = true;
        }*/
    }

    public void ShowConstructionPanel()
    {
       // constructionPanel.SetActive(true);
    }

    public void HideConstructionPanel()
    {
       // constructionPanel.SetActive(false);
    }

}
