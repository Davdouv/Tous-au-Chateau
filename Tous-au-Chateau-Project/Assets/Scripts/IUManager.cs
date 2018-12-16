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
