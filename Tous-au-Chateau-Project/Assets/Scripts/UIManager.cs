using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : PauseScript
{
    //To know where to update the display
    public Text woodTxt;
    public Text stoneTxt;
    public Text foodTxt;
    public Text villagersTxt;
    public Slider motivation;

    //For gameplay purposes
    public GameObject GameOverPanel;
    public Text gameOverVillagersText;
    public ResourceManager _ResourceManager;

    //For construction pagination
    public List<Building> buildings;
    public int constructionNbByPage;
    public GameObject ConstructionPages; //parent of each page content
    private int _nbOfPagesInUI = 0;

    private void Start()
    {
        _nbOfPagesInUI = Mathf.CeilToInt((float)buildings.Count / constructionNbByPage);
    }

    private void Update()
    {
        woodTxt.text = "" + _ResourceManager.GetWood();
        stoneTxt.text = "" + _ResourceManager.GetStone();
        foodTxt.text = "" + _ResourceManager.GetFood();
        villagersTxt.text = "" + _ResourceManager.GetWorkForce();
        motivation.value = _ResourceManager.GetMotivation();
    }

    public void DisplayGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        gameOverVillagersText.text = "Remaining Villagers : " + _ResourceManager.GetWorkForce();
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
