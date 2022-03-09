using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //Note to future self: PLEASE FOR THE LOVE OF GOD MAKE THIS INTO MULTIPLE SCRIPTS

    //How to use:
    /*
    Panels have a specific naming convention requirement
     */

    #region Variables
    [Header("Panels")]
    public GameObject MainUI;
    private List<GameObject> MenuPanels = new List<GameObject>();
    public GameObject topUI;
    public GameObject calandar;
    public GameObject flagStuff;
    public GameObject timeScaler;

    //[Header("Other")]
    public AudioController audioController;
    //List<GameObject> gameObjects = new List<GameObject>();
    //private bool startup = false;
    public bool paused;
    [SerializeField] private Animator transition;
    [SerializeField] private Animator cutsceneTransition;
    [SerializeField] private float transitionTime = 1f;
    Slider progressBar;
    TextMeshProUGUI loadingText;
    CanvasGroup loadingSubtext;
    private float changeLoadingUi = 0f;
    private float minimumLoadingTime = 0f;
    private int elipses = 0;
    private bool isLoading = true;
    private GameObject CutsceneVideoPlayer;
    private bool isCompleted = false;
    [SerializeField] private Image flag;
    private TextMeshProUGUI playerNationText;

    #endregion

    #region Core Functions

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        populatePanels();
        setValues();
        Disable();
        CutsceneVideoPlayer.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 1;
        GameController.Instance.state = eState.TITLE;
        if (GameController.Instance.state == eState.TITLE)  
        {
            VideoPanel.SetActive(true);
            MainMenuPanel.SetActive(true);
        }
        //Start Main Menu Music
    }

    #endregion

    #region Functions

    private void setValues()
    {
        audioController = GameObject.Find("Controllers").GetComponent<AudioController>();
        topUI = GameObject.Find("Top UI");
        calandar = GameObject.Find("Calandar");
        flagStuff = GameObject.Find("Flag Stuff");
        timeScaler = GameObject.Find("TimeScaler");
        eventWindow = GameObject.Find("EventPanel");
        CutsceneVideoPlayer = GameObject.Find("CutsceneVideoPlayer");
        newEvent = GameObject.Find("EventPanel").GetComponent<Event>();
        loadingSubtext = CutscenePanel.transform.Find("LoadingSubtext").gameObject.GetComponentInChildren<CanvasGroup>();
        flag = GameObject.Find("Flag").GetComponent<Image>();
        playerNationText = GameObject.Find("NationNameUI").GetComponent<TextMeshProUGUI>();
        MainUI = GameObject.FindGameObjectWithTag("UI");
    }

    private void populatePanels()
    {
        MainUI.SetActive(true);
        foreach (Transform child in MainUI.transform)
        {
            child.gameObject.SetActive(true);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
                allPanels.Add(grandChild.gameObject);
            }
        }
        foreach (GameObject child in allPanels)
        {
            child.gameObject.SetActive(true);
            if (child.gameObject.CompareTag("UI") && child.name != "LoadingScreenPanel")
            {
                Debug.Log(child.name);
                panels.Add(child.gameObject);
                switch (child.name)
                {
                    case "MainMenuPanel":
                        MainMenuPanel = child.gameObject;
                        break;
                    case "OptionsPanel":
                        OptionsPanel = child.gameObject;
                        break;
                    case "CreditsPanel":
                        CreditsPanel = child.gameObject;
                        break;
                    case "PausePanel":
                        PausePanel = child.gameObject;
                        break;
                    case "InstructionsPanel":
                        InstructionsPanel = child.gameObject;
                        break;
                    case "GamePanel":
                        GamePanel = child.gameObject;
                        break;
                    case "VideoPanel":
                        VideoPanel = child.gameObject;
                        break;
                    case "CutscenePanel":
                        CutscenePanel = child.gameObject;
                        break;
                    default:
                        break;
                }
            }
            else if (child.gameObject.CompareTag("UI") && child.name == "LoadingScreenPanel")
            {
                LoadingScreenPanel = child.gameObject;
                progressBar = LoadingScreenPanel.transform.Find("LoadingBar").gameObject.GetComponent<Slider>();
                loadingText = LoadingScreenPanel.transform.Find("LoadingText").gameObject.GetComponent<TextMeshProUGUI>();
                LoadingScreenPanel.SetActive(false);
            }
        }
    }

    public void Disable()
    {
        foreach (GameObject gameObject in MenuPanels)
        {
            gameObject.SetActive(false);
        }
    }

    //Use the Tag system to locate panels of type and react in an appropriate way
    public void ChangePanel(GameObject panel, bool activate)
    {
        string name = panel.tag;
        //Switch case for name of panel
        Disable();
        switch(name)
        {
            case "MainMenuPanel":
                if (activate) { panel.SetActive(true); } else { panel.SetActive(false); }
                break;
            case "":
                break;
            default:
                break;
        }
    }

    #endregion

    #region Panel Changing

    public void StartGame(GameObject mainMenuPanel)
    {
        ChangePanel(mainMenuPanel, false);
        StartCoroutine(LoadLevel("Game"));
        audioController.Stop("Track" + playing);
        gameTrackPlayer();
        Time.timeScale = 1;
        GameController.Instance.state = eState.LOADING;
    }

    public void ResumeGame()
    {
        Disable();
        Time.timeScale = 1;
        GamePanel.SetActive(true);
        GameController.Instance.state = eState.GAME;
        Debug.Log("Resume Game");
    }

    public void Options()
    {
        Disable();
        OptionsPanel.SetActive(true);
        if (GameController.Instance.state == eState.TITLE)
        {
            VideoPanel.SetActive(true);
        }
        Debug.Log("Options menu");
    }

    public void Instructions()
    {
        Disable();
        InstructionsPanel.SetActive(true);
        if (GameController.Instance.state == eState.TITLE)
        {
            VideoPanel.SetActive(true);
        }
        //GameController.Instance.state = eState.INSTRUCTIONS;
    }

    public void Credits()
    {
        Disable();
        CreditsPanel.SetActive(true);
        if (GameController.Instance.state == eState.TITLE)
        {
            VideoPanel.SetActive(true);
        }
        Debug.Log("Credits menu");
    }
    public void Pause()
    {
        if (GameController.Instance.state == eState.GAME && nationSelected == true)
        {
            Time.timeScale = 0;
            Disable();
            PausePanel.SetActive(true);
            GameController.Instance.state = eState.PAUSE;
        }
    }

    public void calandarPaused()
    {
        if(paused == true && nationSelected == true)
        {
            if(calPausedPanel.active != true) calPausedPanel.SetActive(true);
        }
        else
        {
            if(calPausedPanel.active == true) calPausedPanel.SetActive(false);
        }
    }

    public void displayAudio()
    {
        controlsPanel.gameObject.SetActive(false);
        audiosPanel.gameObject.SetActive(true);
    }

    public void displayKeys()
    {
        audiosPanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(true);
    }

    public void closeProvincePanel()
    {
        provincePanel.SetActive(false);
    }
    private void checkScene()
    {
        long playerCurrentFrame = CutsceneVideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().frame;
        long playerFrameCount = Convert.ToInt64(CutsceneVideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().frameCount)-1;
        if (playerCurrentFrame < playerFrameCount && isCompleted == false)
        {
            Debug.Log("Frame " + playerCurrentFrame + "/" + playerFrameCount);
        }
        else
        {
            
            Debug.Log("Cutscene Completed");
            
            if (isCompleted)
            {
                CutscenePanel.SetActive(false);
                CutsceneVideoPlayer.SetActive(false);
                GamePanel.SetActive(true);
                topUI.SetActive(true);
                calandar.SetActive(true);
                flagStuff.SetActive(true);
                timeScaler.SetActive(true);
                transition.SetTrigger("End");
                playerNationText.text = gameController.playerNation.name;
                Invoke("destroyLoadingScreen", transitionTime);
                isCompleted = false;

            }


        }
    }

    public void Back(bool backToMenu)
    {
        Disable();

        if (!backToMenu)
        {
            //Set Pause Panel to active
            GameController.Instance.state = eState.PAUSE;
        }
        else
        {
            if (SceneManager.GetActiveScene().name != "Main")
            {
                StartCoroutine(LoadLevel("Main"));
            }
            //Game Panel to inactive
            //Main Menu panel to active
            GameController.Instance.state = eState.TITLE;
            //Video Panel to active
        }
    }

    #endregion

    #region System
    public void ResetApplication()
    {
        StartCoroutine(LoadLevel("Main"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion
}