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
            _slider.value = Input.GetAxis("Horizontal");
        }

        if (_joystick == Joystick.VERTICAL)
        {
            _slider.value = Input.GetAxis("Vertical");
        }
    }

    private enum Joystick
    {
        HORIZONTAL,
        VERTICAL,
    }
}
