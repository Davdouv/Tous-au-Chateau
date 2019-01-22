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
    public List<Building> buildings; // pointer sur la liste de building group
    public int constructionNbByPage;
    public GameObject ConstructionPages; //parent of each page content in hierarchy

    private int _nbOfPagesInUI = 0;
    private int _activeConstructionPage = 0;
    private GameObject[] _pages;
    private GameObject[] _pageButtons;

    private void Start()
    {
        _nbOfPagesInUI = Mathf.CeilToInt((float)buildings.Count / constructionNbByPage);
        _pages = new GameObject[_nbOfPagesInUI];
        _pageButtons = new GameObject[_nbOfPagesInUI];

        //Pages
        for(int i = 0; i < _nbOfPagesInUI; ++i)
        {
            GameObject page = Instantiate(new GameObject());
            page.name = "Construction Panel Page " + i;

            //Construction per page
            for(int j = 0; j < constructionNbByPage; ++j)
            {
                if(i * constructionNbByPage + j < buildings.Count)
                    buildings[i * constructionNbByPage + j].transform.parent = page.transform;
            }

            page.transform.parent = ConstructionPages.transform;
            _pages[i] = page;

            if (i != 0)
            {
                page.SetActive(false);
            }
        }

        //Buttons to change active page
        for (int i = 0; i < _nbOfPagesInUI; ++i)
        {
            GameObject button = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            button.name = "Construction Panel Button " + i;
            button.transform.parent = ConstructionPages.transform;

            button.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            button.transform.rotation = ConstructionPages.transform.rotation;
            button.transform.position = ConstructionPages.transform.position + Vector3.forward * 0.02f + Vector3.right * 0.4f + Vector3.up * (0.15f - i * 0.05f);

            _pageButtons[i] = button;
        }
    }

    private void Update()
    {
        woodTxt.text = "" + _ResourceManager.GetWood();
        stoneTxt.text = "" + _ResourceManager.GetStone();
        foodTxt.text = "" + _ResourceManager.GetFood();
        villagersTxt.text = "" + _ResourceManager.GetWorkForce();
        motivation.value = _ResourceManager.GetMotivation();

        //Test for pagination functions
        if (Input.GetKeyUp("right"))
        {
            NextConstructionPage();
        }

        if (Input.GetKeyUp("left"))
        {
            PrevConstructionPage();
        }

        if (Input.GetKey(KeyCode.Keypad0) || Input.GetKey(KeyCode.Alpha0))
        {
            DisplayConstructionPage(0);
        }

        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1))
        {
            DisplayConstructionPage(1);
        }

        if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
        {
            DisplayConstructionPage(2);
        }

        if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Alpha3))
        {
            DisplayConstructionPage(3);
        }

    }

    public void DisplayGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        gameOverVillagersText.text = "Remaining Villagers : " + _ResourceManager.GetWorkForce();
    }

    public void ShowConstructionPanel()
    {
        // constructionPanel.SetActive(true);
    }

    public void HideConstructionPanel()
    {
        // constructionPanel.SetActive(false);
    }

    public void DisplayConstructionPage(int index)
    {
        if (index < 0 || index >= _nbOfPagesInUI)
            return;

        foreach (GameObject page in _pages)
        {
            page.SetActive(false);
        }

        _activeConstructionPage = index;
        _pages[_activeConstructionPage].SetActive(true);
    }

    public void NextConstructionPage()
    {
        if (_activeConstructionPage + 1 >= _nbOfPagesInUI)
            return;

        foreach(GameObject page in _pages)
        {
            page.SetActive(false);
        }

        _activeConstructionPage++;
        _pages[_activeConstructionPage].SetActive(true);
    }

    public void PrevConstructionPage()
    {
        if (_activeConstructionPage - 1 < 0)
            return;

        foreach (GameObject page in _pages)
        {
            page.SetActive(false);
        }

        _activeConstructionPage--;
        _pages[_activeConstructionPage].SetActive(true);
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

}
