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

        #region Singleton
        private static MenuController _instance;

        public static MenuController Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
        #endregion

        #region Variables

        //Controllers
        private AudioController audioController = AudioController.Instance;
        private LevelLoadingController levelLoadingController = LevelLoadingController.Instance;

        //Panels
        private GameObject pausePanel;

        //Vars
        private GameObject CutsceneVideoPlayer;
        private bool paused;

        //Misc
        [SerializeField] private List<Scene> scenes = new List<Scene>();

        //Public
        [Header("System")]
        public List<GameObject> menuPanels = new List<GameObject>();
        //Menu Panel = 0
        //Game Panel = 1
        //Loading Screen Panel = 2

        //Maybe do a seperate cutscene panel here? idfk

        #endregion

        #region Core Functions

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            populatePanels();
            //Disable cutscene panel
            Time.timeScale = 1;
            GameController.Instance.state = eState.TITLE;
            if (GameController.Instance.state == eState.TITLE)
            {
                //If a cutscene is played on start, then set a videopanel to active
                //Set mainMenuPanel to active

            }
            //Start Main Menu Music

            foreach(GameObject panel in menuPanels)
            {
                if(panel.tag == "Pause")
                {
                    pausePanel = panel;
                }
            }
        }

        #endregion

        #region Functions


        private void populatePanels()
        {
            foreach (GameObject child in menuPanels)
            {
                child.SetActive(true);
            }
            Disable();
        }

        public void Disable()
        {
            foreach (GameObject gameObject in menuPanels)
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

        public void Pause()
        {
            if (GameController.Instance.state == eState.GAME)
            {
                Time.timeScale = 0;
                GameController.Instance.state = eState.PAUSE;
                Disable();
                pausePanel.SetActive(true);
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
                    Pause();
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