using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickSlider : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Slider _slider;

    private void Update()
    {
        if (_joystick == Joystick.HORIZONTAL)
        {
            float value = -Input.GetAxis("Horizontal");
            _slider.value = value == 0 ? _slider.value : value;
        }

        if (_joystick == Joystick.VERTICAL)
        {
            float value = Input.GetAxis("Vertical");
            _slider.value = value == 0 ? _slider.value : value;
        }
    }

    private enum Joystick
    {
        HORIZONTAL,
        VERTICAL,
    }
}
