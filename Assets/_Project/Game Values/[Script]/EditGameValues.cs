using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EditGameValues : MonoBehaviour
{
    [SerializeField] private ValueToAssign _valueToAssign;

    [SerializeField] private TextMeshProUGUI _text;
    
    private Slider _slider;
    
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        InitializeSliderValues();
    }

    private void InitializeSliderValues()
    {
        if (_slider)
        {
            switch (_valueToAssign)
            {
                case ValueToAssign.MaxSpeed:
                    _slider.value = GameManager.Instance.GameValues.MaxSpeed;
                    break;
                case ValueToAssign.TiltControlSpeed:
                    _slider.value = GameManager.Instance.GameValues.TiltControlSpeed;
                    break;
                case ValueToAssign.TurningTiltRate:
                    _slider.value = GameManager.Instance.GameValues.TurningTiltRate;
                    break;
                case ValueToAssign.WeightTiltRate:
                    _slider.value = GameManager.Instance.GameValues.WeightTiltRate;
                    break;
                case ValueToAssign.MinObjects:
                    _slider.value = GameManager.Instance.GameValues.MinObjects;
                    break;
                case ValueToAssign.MaxObjects:
                    _slider.value = GameManager.Instance.GameValues.MaxObjects;
                    break;
            }
        }

        _text.text = $"{_slider.value:0.00}";
    }
    
    public void ChangeGameValue(float value)
    {
        switch (_valueToAssign)
        {
            case ValueToAssign.MaxSpeed:
                GameManager.Instance.GameValues.MaxSpeed = value;
                break;
            case ValueToAssign.TiltControlSpeed:
                GameManager.Instance.GameValues.TiltControlSpeed = value;
                break;
            case ValueToAssign.TurningTiltRate:
                GameManager.Instance.GameValues.TurningTiltRate = value;
                break;
            case ValueToAssign.WeightTiltRate:
                GameManager.Instance.GameValues.WeightTiltRate = value;
                break;
            case ValueToAssign.MinObjects:
                GameManager.Instance.GameValues.MinObjects = (int)value;
                break;
            case ValueToAssign.MaxObjects:
                GameManager.Instance.GameValues.MaxObjects = (int)value;
                break;
        }
        
        _text.text = $"{value:0.00}";
    }
    
    public enum ValueToAssign
    {
        MaxSpeed,
        TiltControlSpeed,
        TurningTiltRate,
        WeightTiltRate,
        MinObjects,
        MaxObjects,
    }
}
