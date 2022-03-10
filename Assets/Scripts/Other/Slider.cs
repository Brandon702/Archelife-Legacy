using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    #region Variables
    private AudioController audioController = AudioController.Instance;
    [SerializeField] eAudioType audioType;
    [SerializeField] float sliderValue;

    #endregion

    #region Functions
    public void UpdateLevel()
    {
        audioController.SetLevel(audioType, sliderValue);
        //Save slidervalue data
    }
    #endregion

}
