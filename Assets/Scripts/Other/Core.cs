using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{

    #region Variables
    [SerializeField] private bool isAsync = false;
    private LevelLoadingController levelLoadingController;
    #endregion

    #region Core Functions

    private void Awake()
    {
        isAsync = true;
        levelLoadingController = GameObject.Find("Controllers").GetComponent<LevelLoadingController>();
    }

    #endregion

    #region Functions
    public void ResetApplication()
    {
        if (isAsync)
        {
            StartCoroutine(levelLoadingController.LoadLevel("Main"));
        }
        else
        {
            //Load main normally
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
     #endregion
}
