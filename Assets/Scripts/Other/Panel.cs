using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    #region Variables
    private MenuController menuController = MenuController.Instance;
    #endregion

    #region Core Functions
    private void Start()
    {
        menuController.menuPanels.Add(gameObject);
    }
    #endregion
}
