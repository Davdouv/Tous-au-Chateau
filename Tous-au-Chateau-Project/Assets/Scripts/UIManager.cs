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

    public GameObject gameOverPanel;
    public Text gameOverVillagersText;
    //public GameObject constructionPanel;
    public ResourceManager resourceManager;

	void Start () {
        woodTxt.text = "" + resourceManager.GetWood();
        stoneTxt.text = "" + resourceManager.GetStone();
        foodTxt.text = "" + resourceManager.GetFood();
        villagersTxt.text = "" + resourceManager.GetVillagers();
        motivation.value = resourceManager.GetMotivation();
    }

    private void Update()
    {
        woodTxt.text = "" + resourceManager.GetWood();
        stoneTxt.text = "" + resourceManager.GetStone();
        foodTxt.text = "" + resourceManager.GetFood();
        villagersTxt.text = "" + resourceManager.GetVillagers();
        motivation.value = resourceManager.GetMotivation();

        /* For testing hide and show purposes */
        /*if (Input.GetKeyDown("space"))
        {
            ShowConstructionPanel();
        }

        if (Input.GetKeyUp("space"))
        {
            HideConstructionPanel();
        }*/
    }

    public void DisplayGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverVillagersText.text = "Remaining Villagers : " + resourceManager.GetVillagers();
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

    /* WOOD */
    public void IncreaseWood()
    {
        resourceManager.AddWood(10);
    }

    public void DecreaseWood()
    {
        resourceManager.RemoveWood(7);
    }

    /* STONE */
    public void IncreaseStone()
    {
        resourceManager.AddStone(10);
    }

    public void DecreaseStone()
    {
        resourceManager.RemoveStone(9);
    }

    /* FOOD */
    public void IncreaseFood()
    {
        resourceManager.AddFood(5);
    }

    public void DecreaseFood()
    {
        resourceManager.RemoveFood(7);
    }

    /* VILLAGERS */
    public void IncreaseVillagers()
    {
        resourceManager.AddVillagers(3);
    }

    public void DecreaseVillagers()
    {
        resourceManager.RemoveVillagers(2);
    }

    /* MOTIVATION */
    public void IncreaseMotivation()
    {
        resourceManager.AddMotivation(10);
    }

    public void DecreaseMotivation()
    {
        resourceManager.RemoveMotivation(10);
    }
}
