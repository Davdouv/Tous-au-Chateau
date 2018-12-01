using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IUManager : MonoBehaviour {

    public Text woodTxt;
    public Text stoneTxt;
    public Text foodTxt;
    public Text villagersTxt;
    public Slider motivation;

    public ResourceManager resourceManager;

	void Start () {
        woodTxt.text = "Wood : " + resourceManager.GetWood();
        stoneTxt.text = "Stone : " + resourceManager.GetStone();
        foodTxt.text = "Food : " + resourceManager.GetFood();
        villagersTxt.text = "Villagers : " + resourceManager.GetVillagers();
        motivation.value = resourceManager.GetMotivation();
    }

    private void Update()
    {
        woodTxt.text = "Wood : " + resourceManager.GetWood();
        stoneTxt.text = "Stone : " + resourceManager.GetStone();
        foodTxt.text = "Food : " + resourceManager.GetFood();
        villagersTxt.text = "Villagers : " + resourceManager.GetVillagers();
        motivation.value = resourceManager.GetMotivation();
    }

    /* WOOD */
    public void IncreaseWood()
    {
        resourceManager.SetWood(resourceManager.GetWood() + 10);
    }

    public void DecreaseWood()
    {
        resourceManager.SetWood(resourceManager.GetWood() - 5);
    }

    /* STONE */
    public void IncreaseStone()
    {
        resourceManager.SetStone(resourceManager.GetStone() + 10);
    }

    public void DecreaseStone()
    {
        resourceManager.SetStone(resourceManager.GetStone() - 10);
    }

    /* FOOD */
    public void IncreaseFood()
    {
        resourceManager.SetFood(resourceManager.GetFood() + 5);
    }

    public void DecreaseFood()
    {
        resourceManager.SetFood(resourceManager.GetFood() - 5);
    }

    /* VILLAGERS */
    public void IncreaseVillagers()
    {
        resourceManager.SetVillagers(resourceManager.GetVillagers() + 3);
    }

    public void DecreaseVillagers()
    {
        resourceManager.SetVillagers(resourceManager.GetVillagers() - 1);
    }

    /* MOTIVATION */
    public void IncreaseMotivation()
    {
        resourceManager.SetMotivation(resourceManager.GetMotivation() + 10);
    }

    public void DecreaseMotivation()
    {
        resourceManager.SetMotivation(resourceManager.GetMotivation() - 10);
    }
}
