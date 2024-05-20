using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleController : MonoBehaviour
{
    private Toggle _toggle;
    [SerializeField] private ToggleType _toggleType;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        switch (_toggleType)
        {
            case ToggleType.InvertAxisX:
                _toggle.isOn = PlayerPrefs.GetInt("InvertAxisX") == 1;
                break;
            
            case ToggleType.InvertAxisY:
                _toggle.isOn = PlayerPrefs.GetInt("InvertAxisY") == 1;
                break;
        }
    }

    public void ToggleAxisX(bool ToggleValue)
    {
        if (ToggleValue)
        {
            PlayerPrefs.SetInt("InvertAxisX",1);
        }
        else
        {
            PlayerPrefs.SetInt("InvertAxisX",0);
            
        }
    }
    
    public void ToggleAxisY(bool ToggleValue)
    {
        if (ToggleValue)
        {
            PlayerPrefs.SetInt("InvertAxisY",1);
            
        }
        else
        {
            PlayerPrefs.SetInt("InvertAxisY",0);
        }
    }
    
    public enum ToggleType
    {
        InvertAxisX,
        InvertAxisY
    }
}
