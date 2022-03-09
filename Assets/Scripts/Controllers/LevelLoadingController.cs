using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadingController : MonoBehaviour
{




    #region Functions
    private IEnumerator LoadLevel(string levelName)
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


    #endregion
}
