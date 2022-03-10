using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {

        #region Variables

        //Controllers
        private AudioController audioController;
        private LevelLoadingController levelLoadingController;

        //Vars
        private GameObject MainUI;
        private GameObject CutsceneVideoPlayer;
        private List<GameObject> MenuPanels = new List<GameObject>();
        private bool paused;
        private PanelType panel;

        [Header("System")]
        [SerializeField] private List<Scene> scenes = new List<Scene>();

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
                //If a cutscene is played on start, then set a videopanel to active
                //Set mainMenuPanel to active

            }
            //Start Main Menu Music
        }

        #endregion

        #region Functions

        private void setValues()
        {
            audioController = GameObject.Find("Controllers").GetComponent<AudioController>();
            CutsceneVideoPlayer = GameObject.Find("CutsceneVideoPlayer");
            levelLoadingController.loadingSubtext = CutscenePanel.transform.Find("LoadingSubtext").gameObject.GetComponentInChildren<CanvasGroup>();
            MainUI = GameObject.FindGameObjectWithTag("UI");
        }

        private void populatePanels()
        {
            List<GameObject> allPanels = new List<GameObject>();
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
                    MenuPanels.Add(child.gameObject);
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
                    //LoadingScreenPanel = child.gameObject;
                    //progressBar = LoadingScreenPanel.transform.Find("LoadingBar").gameObject.GetComponent<Slider>();
                    //loadingText = LoadingScreenPanel.transform.Find("LoadingText").gameObject.GetComponent<TextMeshProUGUI>();
                    //Disable loading screen panel
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

        public void QuickToggle(GameObject panel, bool enable)
        {
            if(enable)
            {
                Disable();
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
            
        }

        public void EnablePanel(GameObject panel)
        {
            switch(panel.tag)
            {
                case "MainMenu":
                    GameController.Instance.state = eState.TITLE;
                    QuickToggle(panel, true);
                    break;
                case "Resume":
                    GameController.Instance.state = eState.GAME;
                    Time.timeScale = 1;
                    QuickToggle(panel, true);
                    break;
                case "Options":
                    QuickToggle(panel, true);
                    break;
                case "Instructions":
                    QuickToggle(panel, true);
                    break;
                case "Credits":
                    QuickToggle(panel, true);
                    break;
                case "Pause":
                    if (GameController.Instance.state == eState.GAME)
                    {
                        Time.timeScale = 0;
                        GameController.Instance.state = eState.PAUSE;
                        QuickToggle(panel, true);
                    }
                    break;
                default:
                    break;
            }

            if (GameController.Instance.state == eState.TITLE)
            {
                //Set main menu videopanel to active
            }
        }

        public void DisablePanel(GameObject panel)
        {
            Disable();
            panel.SetActive(false);
        }

        public void StartGame(GameObject mainMenuPanel)
        {
            EnablePanel(mainMenuPanel);
            levelLoadingController.LoadWithCoroutine("Game");
            Time.timeScale = 1;
            GameController.Instance.state = eState.LOADING;
        }

        //Back to Menu, if not then pack to pause
        public void GoBack(bool backToMainMenu, bool Dynamic)
        {
            if(!Dynamic)
            {
                Disable();
                if (!backToMainMenu)
                {
                    //Set Pause Panel to active
                    GameController.Instance.state = eState.PAUSE;
                }
                else
                {
                    if (SceneManager.GetActiveScene().name != "Main")
                    {
                        levelLoadingController.LoadWithCoroutine("Main");
                    }
                    //Game Panel to inactive
                    //Main Menu panel to active
                    GameController.Instance.state = eState.TITLE;
                    //Video Panel to active
                }
            }
            else
            {
                if(GameController.Instance.state == eState.TITLE)
                {

                }
            }
            
        }

        #endregion
    }
}