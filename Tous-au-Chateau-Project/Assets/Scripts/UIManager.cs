using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//IMPORTANT LEAVE THIS LINE HERE
//VISUAL STUDIO DOESN'T UNDERSTANDS IT
//BUT IT HAS IMPACT ON THE SCRIPT
using TMPro;

public class UIManager : MonoBehaviour
{

    #region Singleton
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("_UIManager");
                go.AddComponent<UIManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }
    #endregion

    //Reference to resources display
    public Text woodTxt;
    public Text stoneTxt;
    public Text foodTxt;
    public Text villagersTxt;
    public Slider motivation;

    //For gameplay purposes
    public GameObject GameOverPanel;
    public Text gameOverTitleText;
    public Text gameOverVillagersText;
    public Button gameOverButton;
    public Color victoryTextColor;
    public Color gameoverTextColor;

    public GameObject controlPanel;
    public GameObject ResourceGainPrefab;

    //For construction pagination
    public GameObject ConstructionPagination; //parent of each page content in hierarchy
    public Color buildingNotPuchasable;
    public GameObject paginationButtonsPrefab;
    public Transform buttonsPosition;
    public Transform constructionPosition1;
    public Transform constructionPosition2;
    public Transform constructionPosition3;
    public Transform constructionPosition4;

    private List<List<Building>> _sortedBuildings; //sorted by type
    private const int constructionNbByPage = 4;
    private int _nbOfPagesInUI = 0;
    private int _activeConstructionPage = 0;
    private GameObject[] _pages;
    private GameObject[] _pageButtons;
    private bool _isCostEmpty = true;
    private Transform[] constructionsPositions;

    private void Start()
    {
        constructionsPositions = new Transform[4];
        constructionsPositions[0] = constructionPosition1;
        constructionsPositions[1] = constructionPosition2;
        constructionsPositions[2] = constructionPosition3;
        constructionsPositions[3] = constructionPosition4;

        if (BuildingsTypeGroup.Instance != null)
        {
            CalculateNbOfPages();
        }
    }

    private void Update()
    {
        if (BuildingsTypeGroup.Instance != null && _pages == null && _pageButtons == null) //if fails on start
        {
            CalculateNbOfPages();
        }
        else if (_isCostEmpty) //in case the costs are empty the info still needs to be updated
        {
            UpdateBuildingInfo();
        }

        /* Test for end of game */
        if (GameManager.Instance.IsGameWon())
        {
            DisplayGameOverPanel(true);
        }

        if (GameManager.Instance.IsGameLost())
        {
            DisplayGameOverPanel(false);
        }

        //prevent calculation if panel is closed
        if (!controlPanel.activeSelf)
        {
            return;
        }

        UpdateResourcesInformation();
        CheckBuildingsdAvailability();

        CheckComputerPaginationInputs();
    }

    public void DisplayGameOverPanel(bool isPlayerVictorious)
    {
        if (isPlayerVictorious)
        {
            gameOverTitleText.text = "VICTORY";
            gameOverTitleText.color = victoryTextColor;

            //button
            //Changes the button's Normal color to the right color.
            ColorBlock cb = gameOverButton.colors;
            cb.normalColor = victoryTextColor;
            gameOverButton.colors = cb;

            //change the button's text
            Transform buttonText = gameOverButton.transform.GetChild(0);
            if(buttonText != null)
            {
                buttonText.GetComponent<Text>().text = "Continue";
            }

            //loads next tuto or mapselector after victory
            gameOverButton.onClick.AddListener(() => { SceneManager.LoadScene(GameManager.Instance.nextSceneName); });
            
        }
        else
        {
            gameOverTitleText.text = "GAME OVER";
            gameOverTitleText.color = gameoverTextColor;

            //button
            //Changes the button's Normal color to the right color.
            ColorBlock cb = gameOverButton.colors;
            cb.normalColor = gameoverTextColor;
            gameOverButton.colors = cb;

            //change the button's text
            Transform buttonText = gameOverButton.transform.GetChild(0);
            if (buttonText != null)
            {
                buttonText.GetComponent<Text>().text = "Restart";
            }

            //change the button onclick function
            //reloads the current scene
            gameOverButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
        }

        GameOverPanel.SetActive(true);
        gameOverVillagersText.text = "Remaining Villagers : " + ResourceManager.Instance.GetWorkForce();
    }

    private void UpdateResourcesInformation()
    {
        woodTxt.text = "" + ResourceManager.Instance.GetWood();
        stoneTxt.text = "" + ResourceManager.Instance.GetStone();
        foodTxt.text = "" + ResourceManager.Instance.GetFood();
        villagersTxt.text = "" + ResourceManager.Instance.GetWorkForce();
        motivation.value = ResourceManager.Instance.GetMotivation();
    }

    /* Updates the ability to purchase or not each building */
    private void CheckBuildingsdAvailability()
    {
        for (int i = 0; i < _sortedBuildings.Count; ++i)
        {
            for (int j = 0; j < _sortedBuildings[i].Count; ++j)
            {
                Building currentBuilding = _sortedBuildings[i][j];
                if (ResourceManager.Instance.HasEnoughResources(currentBuilding.getCost()))
                {
                    //make it normal
                    currentBuilding.transform.GetChild(2).gameObject.SetActive(true);
                    currentBuilding.transform.GetChild(3).gameObject.SetActive(false);

                    Transform cost = currentBuilding.transform.Find("Display/HelpTextCanvas/Cost");
                    if (cost != null)
                    {
                        //IMPORTANT THIS LINE IS NOT AN ERROR
                        //VISUAL STUDIO DOESN'T UNDERSTANDS IT BUT IT WORKS
                        cost.GetComponent<TextMeshProUGUI>().color = Color.black;
                    }
                }
                else
                {
                    //make it blocked
                    currentBuilding.transform.GetChild(2).gameObject.SetActive(false);
                    currentBuilding.transform.GetChild(3).gameObject.SetActive(true);

                    Transform cost = currentBuilding.transform.Find("Display/HelpTextCanvas/Cost");
                    if (cost != null)
                    {
                        //IMPORTANT THIS LINE IS NOT AN ERROR
                        //VISUAL STUDIO DOESN'T UNDERSTANDS IT BUT IT WORKS
                        cost.GetComponent<TextMeshProUGUI>().color = buildingNotPuchasable;
                    }
                }
            }
        }
    }

    /* For testing purposes */
    private void CheckComputerPaginationInputs()
    {
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

    public void ShowConstructionPanel()
    {
        // constructionPanel.SetActive(true);
    }

    public void HideConstructionPanel()
    {
        // constructionPanel.SetActive(false);
    }

    /* Pagination */
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
        List<Building> tmpBuilding = BuildingsTypeGroup.Instance.buildings;

        for (int i = 0; i < tmpBuilding.Count; ++i)
        {
            Transform title = tmpBuilding[i].transform.Find("Title/TitleCanvas/TitleText");

            if(title != null)
            {
                title.GetComponent<Text>().text = tmpBuilding[i]._name;
            }

            Transform cost = tmpBuilding[i].transform.Find("Display/HelpTextCanvas/Cost");

            if (cost != null && tmpBuilding[i].GetCostString() != "")
            {
                //IMPORTANT THIS LINE IS NOT AN ERROR
                //VISUAL STUDIO DOESN'T UNDERSTANDS IT BUT IT WORKS
                cost.GetComponent<TextMeshProUGUI>().text = tmpBuilding[i].GetCostString();
                _isCostEmpty = _isCostEmpty || false;
            }
        }
    }

    //only for beginning of program
    private void CalculateNbOfPages()
    {
        _sortedBuildings = BuildingsTypeGroup.Instance.getBuildingsSortedByType();

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
                        _sortedBuildings[i][currentIndexInPage].transform.position = constructionsPositions[k].position;
                    }
                }

                page.transform.position = Vector3.zero /*+ Vector3.up * 0.07f / page.transform.localScale.y*/;
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
            GameObject button = Instantiate(paginationButtonsPrefab);
            button.name = "Construction Panel Button " + i;
            button.transform.parent = buttonsPosition;

            //Change umber in 3D text child
            Transform textNb = button.transform.Find("Canvas/Page number");

            if (textNb != null)
            {
                textNb.GetComponent<Text>().text = "" + (i+1);
            }

            button.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            button.transform.position = buttonsPosition.position 
                + Vector3.forward * 0.02f / 0.01f
                + Vector3.up * (- i * 0.08f) / 0.01f;

            _pageButtons[i] = button;
        }
    }

}
