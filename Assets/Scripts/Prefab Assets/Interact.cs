using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    #region Variables
    bool active = false;
    GameController gameController = GameController.Instance;
    #endregion

    #region Core Functions
    private void Update()
    {
        //Run code here
    }
    #endregion

    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        active = true;
    }

    private void OnTriggerExit(Collider other)
    {
        active = false;
    }

    #endregion
}
