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
    public BuildingsTypeGroup _BuildingTypeGroup;
    public GameObject ConstructionPagination; //parent of each page content in hierarchy

    private List<List<Building>> _sortedBuildings; //sorted by type
    private const int constructionNbByPage = 4;
    private int _nbOfPagesInUI = 0;
    private int _activeConstructionPage = 0;
    private GameObject[] _pages;
    private GameObject[] _pageButtons;
    private bool _isCostEmpty = true;

    private void Start()
    {
        if(_BuildingTypeGroup != null)
        {
            CalculateNbOfPages();
        }
    }

    private void Update()
    {
        if (_BuildingTypeGroup != null && _pages == null && _pageButtons == null) //if fails on start
        {
            CalculateNbOfPages();
        }
        else if (_isCostEmpty) //in case the costs are empty the info still needs to be updated
        {
            UpdateBuildingInfo();
        }

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
        //End of test for pagination

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

    //only used at beginning of program
    private void UpdateBuildingInfo()
    {
        for(int i = 0; i < _BuildingTypeGroup._buildings.Count; ++i)
        {
            Transform title = _BuildingTypeGroup._buildings[i].transform.Find("Title/TitleCanvas/TitleText");

            if(title != null)
            {
                title.GetComponent<Text>().text = _BuildingTypeGroup._buildings[i]._name;
            }

            Transform cost = _BuildingTypeGroup._buildings[i].transform.Find("Display/HelpTextCanvas/Cost");

            if (cost != null && _BuildingTypeGroup._buildings[i].GetCostString() != "")
            {
                cost.GetComponent<Text>().text = _BuildingTypeGroup._buildings[i].GetCostString();
                _isCostEmpty = _isCostEmpty || false;
            }
        }
    }

    //only for beginning of program
    private void CalculateNbOfPages()
    {
        _sortedBuildings = _BuildingTypeGroup.getBuildingsSortedByType();

        if (_sortedBuildings == null)
            return;

        for(int i = 0; i < _sortedBuildings.Count; ++i)
        {
            _nbOfPagesInUI += Mathf.CeilToInt((float)_sortedBuildings[i].Count / constructionNbByPage);
        }

        _pages = new GameObject[_nbOfPagesInUI];
        _pageButtons = new GameObject[_nbOfPagesInUI];

        UpdateBuildingInfo();
        CreatePagesList();
        CreateButtonsList();
    }

    //only used at beginning of program
    private void CreatePagesList ()
    {
        int currentPageIndex = 0;

        for (int i = 0; i < _sortedBuildings.Count; ++i)
        {
            //may be several pages per type
            int nbOfPageForType = Mathf.CeilToInt((float)_sortedBuildings[i].Count / constructionNbByPage);

            for(int j = 0; j < nbOfPageForType; ++j)
            {
                GameObject page = Instantiate(new GameObject());
                page.name = "Construction Panel Page " + currentPageIndex;
                page.transform.parent = ConstructionPagination.transform;

                //Construction per page
                for (int k = 0; k < constructionNbByPage; ++k)
                {
                    int currentIndexInPage = j * constructionNbByPage + k;
                    if (currentIndexInPage < _sortedBuildings[i].Count)
                    {
                        _sortedBuildings[i][currentIndexInPage].transform.parent = page.transform;
                        _sortedBuildings[i][currentIndexInPage].transform.position = ConstructionPagination.transform.position + Vector3.right * ((k * 0.2f - 0.3f) * -1.0f) / page.transform.localScale.x;
                    }
                }

                page.transform.position = Vector3.zero + Vector3.up * 0.07f / page.transform.localScale.y;
                _pages[i] = page;

                if (i != 0)
                {
                    page.SetActive(false);
                }

                ++currentPageIndex;
            }
        }
    }

    //only used at beginning of program
    private void CreateButtonsList ()
    {
        if (_nbOfPagesInUI <= 1)
            return;

        for (int i = 0; i < _nbOfPagesInUI; ++i)
        {
            GameObject button = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            button.name = "Construction Panel Button " + i;
            button.transform.parent = ConstructionPagination.transform;

            button.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            button.transform.position = ConstructionPagination.transform.position 
                + Vector3.forward * 0.02f / 0.01f 
                + Vector3.right * 0.44f / 0.01f
                + Vector3.up * (0.15f - i * 0.05f) / 0.01f;

            _pageButtons[i] = button;
        }
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
