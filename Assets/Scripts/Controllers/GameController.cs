using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public enum eState
    {
        TITLE,
        GAME,
        PAUSE,
        LOADING,
        CUTSCENE
    }

    public class GameController : MonoBehaviour
    {

        #region Singleton
        private static GameController _instance;

        public static GameController Instance
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

        #region Core Functions

        private void Start()
        {
            menuController = GameObject.Find("Controllers").GetComponent<MenuController>();
        }

        private void Update()
        {
            if (state == eState.GAME)
            {

                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                {
                    //Pause Game
                    menuController.EnablePanel();
                }
            }
        }
        #endregion

        #region Variables
        public eState state;
        MenuController menuController;
        #endregion
    }
}