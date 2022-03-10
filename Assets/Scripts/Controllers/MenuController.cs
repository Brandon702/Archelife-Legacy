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
        public void EnablePanel(GameObject panel)
        {
            Disable();
            panel.SetActive(true);
            //Main Menu Cutscene Panel set active if state = title
        }

        public void DisablePanel(GameObject panel)
        {
            Disable();
            panel.SetActive(false);
        }

        #endregion

        #region Panel Changing

        public void StartGame(GameObject mainMenuPanel)
        {
            EnablePanel(mainMenuPanel);
            levelLoadingController.LoadWithCoroutine("Game");
            Loaded
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
        //Options, Instructions, Credits, Pause
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
                    levelLoadingController.LoadWithCoroutine("Main");
                }
                //Game Panel to inactive
                //Main Menu panel to active
                GameController.Instance.state = eState.TITLE;
                //Video Panel to active
            }
        }

        #endregion
    }
}