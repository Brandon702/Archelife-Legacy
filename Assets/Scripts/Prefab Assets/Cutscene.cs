using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    #region Variables
    public CanvasGroup loadingSubtext;
    TextMeshProUGUI loadingText;
    public float transitionTime;
    #endregion

    #region Functions
    private void Start()
    {
        //loadingSubtext = CutscenePanel.transform.Find("LoadingSubtext").gameObject.GetComponentInChildren<CanvasGroup>();
    }

    #endregion

}
