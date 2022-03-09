using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    #region Variables
    private bool activated;

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

    private IEnumerator LoadCutscene()
    {
        LoadingScreenPanel.transform.Find("LoadingText").gameObject.SetActive(false);
        LoadingScreenPanel.transform.Find("LoadingBar").gameObject.SetActive(false);
        LoadingScreenPanel.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("Cutscene Start");
        NationSelection.SetActive(false);
        CutscenePanel.SetActive(true);
        CutsceneVideoPlayer.SetActive(true);
        CutsceneVideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        cutsceneTransition.SetTrigger("Start");
        GameController.Instance.state = eState.CUTSCENE;
        InvokeRepeating("checkScene", 0.1f, 0.1f);
    }

    #endregion
}
