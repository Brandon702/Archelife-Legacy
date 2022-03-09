using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LevelLoadingController : MonoBehaviour
    {
        #region Variables

        bool isLoading = true;
        int elipses = 0;
        [SerializeField] private float transitionTime = 1f;
        [SerializeField] private Animator transition;
        [SerializeField] private Animator cutsceneTransition;
        TextMeshProUGUI loadingText;
        CanvasGroup loadingSubtext;
        private float changeLoadingUi = 0f;
        private float minimumLoadingTime = 0f;
        Slider progressBar;

        #endregion

        #region Functions
        public IEnumerator LoadLevel(string levelName)
        {
            isLoading = true;
            elipses = 0;
            LoadingScreenPanel.transform.Find("LoadingText").gameObject.SetActive(false);
            LoadingScreenPanel.transform.Find("LoadingBar").gameObject.SetActive(false);
            LoadingScreenPanel.SetActive(true);
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            Disable();
            if (levelName == "Main")
            {
                var scene = GameObject.Find("Scene").GetComponent<Transform>();
                Destroy(scene.root.gameObject);
            }
            else
            {
                LoadingScreenPanel.transform.Find("LoadingText").gameObject.SetActive(true);
                LoadingScreenPanel.transform.Find("LoadingBar").gameObject.SetActive(true);
            }
            GamePanel.SetActive(true);
            topUI.SetActive(true);
            calandar.SetActive(true);
            flagStuff.SetActive(true);
            timeScaler.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

            loadingText.text = "Loading Game...";
            changeLoadingUi = 3;
            while (isLoading)
            {
                changeLoadingUi += Time.deltaTime;
                minimumLoadingTime += Time.deltaTime;
                if (changeLoadingUi > 0.7f)
                {
                    TextChange();
                    changeLoadingUi = 0;
                }
                if (operation.progress < minimumLoadingTime)
                {
                    progressBar.value = operation.progress;
                }
                else
                {
                    progressBar.value = minimumLoadingTime / 3;
                }

                if (operation.isDone && minimumLoadingTime >= 3f)
                {
                    isLoading = false;
                    transition.SetTrigger("End");

                    LoadingScreenPanel.transform.Find("LoadingText").gameObject.SetActive(false);
                    LoadingScreenPanel.transform.Find("LoadingBar").gameObject.SetActive(false);
                    yield return new WaitForSeconds(transitionTime);
                    GameController.Instance.state = eState.GAME;
                    LoadingScreenPanel.SetActive(false);
                }
                yield return 0;
            }

        }

        private void TextChange()
        {
            if (elipses < 3)
            {
                elipses++;
                switch (elipses)
                {
                    case 1:
                        loadingText.text = "Loading Game.";
                        break;
                    case 2:
                        loadingText.text = "Loading Game..";
                        break;
                    case 3:
                        loadingText.text = "Loading Game...";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                elipses = 1;
                loadingText.text = "Loading Game.";
            }
        }

        private void destroyLoadingScreen()
        {
            LoadingScreenPanel.SetActive(false);
            GameController.Instance.state = eState.GAME;
            gameTrackPlayer();
            CancelInvoke();
        }

        private void checkScene()
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


        #endregion
    }
}