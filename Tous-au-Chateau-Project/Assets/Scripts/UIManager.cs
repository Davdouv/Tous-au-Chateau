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
    //public GameObject constructionPanel;
    public ResourceManager ResourceManager;

	void Start () {
        woodTxt.text = "" + ResourceManager.GetWood();
        stoneTxt.text = "" + ResourceManager.GetStone();
        foodTxt.text = "" + ResourceManager.GetFood();
        villagersTxt.text = "" + ResourceManager.GetVillagers();
        motivation.value = ResourceManager.GetMotivation();
    }

    private void Update()
    {
        woodTxt.text = "" + ResourceManager.GetWood();
        stoneTxt.text = "" + ResourceManager.GetStone();
        foodTxt.text = "" + ResourceManager.GetFood();
        villagersTxt.text = "" + ResourceManager.GetVillagers();
        motivation.value = ResourceManager.GetMotivation();

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
        GameOverPanel.SetActive(true);
        gameOverVillagersText.text = "Remaining Villagers : " + ResourceManager.GetVillagers();
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
        ResourceManager.AddWood(10);
    }

    public void DecreaseWood()
    {
        ResourceManager.RemoveWood(7);
    }

    /* STONE */
    public void IncreaseStone()
    {
        ResourceManager.AddStone(10);
    }

    public void DecreaseStone()
    {
        ResourceManager.RemoveStone(9);
    }

    /* FOOD */
    public void IncreaseFood()
    {
        ResourceManager.AddFood(5);
    }

    public void DecreaseFood()
    {
        ResourceManager.RemoveFood(7);
    }

    /* VILLAGERS */
    public void IncreaseVillagers()
    {
        ResourceManager.AddVillagers(3);
    }

    public void DecreaseVillagers()
    {
        ResourceManager.RemoveVillagers(2);
    }

    /* MOTIVATION */
    public void IncreaseMotivation()
    {
        ResourceManager.AddMotivation(10);
    }

    public void DecreaseMotivation()
    {
        ResourceManager.RemoveMotivation(10);
    }
}
