using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class LevelLoadingController : MonoBehaviour
    {

        #region Singleton
        private static LevelLoadingController _instance;

        public static LevelLoadingController Instance
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
        MenuController menuController = MenuController.Instance;
        bool isLoading = true;
        [SerializeField] private float transitionTime = 1f;
        [SerializeField] private Animator transition;
        [SerializeField] private Animator sceneTransition;
        [SerializeField] private float minimumLoadingTime = 0f;
        private float loadingTime = 0;
        Slider progressBar;

        #endregion

        #region Functions

        public void LoadWithCoroutine(string levelName)
        {
            StartCoroutine(LoadLevel(levelName));
        }

        public IEnumerator LoadLevel(string levelName)
        {
            isLoading = true;
            menuController.menuPanels[0].transform.Find("LoadingText").gameObject.SetActive(false);
            menuController.menuPanels[0].transform.Find("LoadingBar").gameObject.SetActive(false);
            menuController.menuPanels[0].SetActive(true);
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            menuController.Disable();
            if (levelName == "Main")
            {
                var scene = GameObject.Find("Scene").GetComponent<Transform>();
                Destroy(scene.root.gameObject);
            }
            else
            {
                menuController.menuPanels[0].transform.Find("LoadingText").gameObject.SetActive(true);
                menuController.menuPanels[0].transform.Find("LoadingBar").gameObject.SetActive(true);
            }
            menuController.menuPanels[0].SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

            while (isLoading)
            {
                
                loadingTime += Time.deltaTime;
                if (operation.isDone && loadingTime >= minimumLoadingTime)
                {
                    isLoading = false;
                    transition.SetTrigger("End");

                    menuController.menuPanels[0].transform.Find("LoadingText").gameObject.SetActive(false);
                    menuController.menuPanels[0].transform.Find("LoadingBar").gameObject.SetActive(false);
                    yield return new WaitForSeconds(transitionTime);
                    GameController.Instance.state = eState.GAME;
                    menuController.menuPanels[0].SetActive(false);
                }
                yield return 0;
            }

        }

        private void destroyLoadingScreen()
        {
            menuController.menuPanels[0].SetActive(false);
            GameController.Instance.state = eState.GAME;
            CancelInvoke();
        }
        #endregion
    }
}