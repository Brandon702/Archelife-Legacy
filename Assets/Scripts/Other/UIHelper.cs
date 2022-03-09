using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHelper : MonoBehaviour
{
    public void backButtonPress()
    {
        Transform scene = GameObject.Find("Scene").GetComponent<Transform>();
        MenuController menuController = GameObject.Find("Controllers").GetComponent<MenuController>();
        menuController.Back();
        SceneManager.MoveGameObjectToScene(scene.gameObject, SceneManager.GetActiveScene());
        SceneManager.LoadScene("Main");
        //StartCoroutine(menuController.LoadLevel("Main"));
    }

    public void Play()
    {
        MenuController menuController = GameObject.Find("Controllers").GetComponent<MenuController>();
        menuController.audioController.Play("buttonClick");
    }
}
