using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class CutsceneController : MonoBehaviour
    {

        #region Singleton
        private static CutsceneController _instance;

        public static CutsceneController Instance
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
        private bool activated;
        private bool isCompleted = false;
        [SerializeField] private Animator cutsceneTransition;
        [SerializeField] CanvasGroup loadingSubtext;
        [SerializeField] Animator transition;
        [SerializeField] GameObject CutsceneVideoPlayer;

        #endregion

        #region Core Functions
        public void Update()
        {

            if (GameController.Instance.state == eState.CUTSCENE)
            {
                SkipCutscene();
            }
        }

        #endregion

        #region Functions
        public void SkipCutscene()
        {

            if (Input.anyKeyDown)
            {
                if (activated)
                {
                    isCompleted = true;
                }
                cutsceneTransition.SetTrigger("Start");
            }

            if (loadingSubtext.alpha > 0.2f)
            {
                activated = true;
            }
            else
            {
                activated = false;
            }
        }

        private IEnumerator LoadCutscene(Cutscene cutscene)
        {
            menuController.menuPanels[2].transform.Find("LoadingText").gameObject.SetActive(false);
            menuController.menuPanels[2].transform.Find("LoadingBar").gameObject.SetActive(false);
            menuController.menuPanels[2].SetActive(true);
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(cutscene.transitionTime);
            Debug.Log("Cutscene Start");
            //Enable the selected cutscene panel
            CutsceneVideoPlayer.SetActive(true);
            CutsceneVideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            cutsceneTransition.SetTrigger("Start");
            GameController.Instance.state = eState.CUTSCENE;
            InvokeRepeating("checkScene", 0.1f, 0.1f);
        }

        private void checkScene(Cutscene cutscene)
        {
            long playerCurrentFrame = CutsceneVideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().frame;
            long playerFrameCount = Convert.ToInt64(CutsceneVideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().frameCount) - 1;
            if (playerCurrentFrame < playerFrameCount && isCompleted == false)
            {
                Debug.Log("Frame " + playerCurrentFrame + "/" + playerFrameCount);
            }
            else
            {

                Debug.Log("Cutscene Completed");

                if (isCompleted)
                {
                    //disable the active cutscene panel here
                    CutsceneVideoPlayer.SetActive(false);
                    menuController.menuPanels[1].SetActive(true);
                    transition.SetTrigger("End");
                    Invoke("destroyLoadingScreen", cutscene.transitionTime);
                    isCompleted = false;

                }


            }

            #endregion
        }
    }
}