using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ToggleController : MonoBehaviour
{
    public void ToggleAxisX(bool ToggleValue)
    {
        if (ToggleValue)
        {
            PlayerPrefs.SetInt("InvertedAxisX",1);
            
        }
        else
        {
            PlayerPrefs.SetInt("InvertedAxisX",0);
            
        }
    }
    
    public void ToggleAxisY(bool ToggleValue)
    {
        if (ToggleValue)
        {
            PlayerPrefs.SetInt("InvertedAxisY",1);
            
        }
        else
        {
            PlayerPrefs.SetInt("InvertedAxisY",0);
            
        }
    }
}
