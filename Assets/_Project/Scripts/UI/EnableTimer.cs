using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnableTimer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _button;
    [SerializeField] private float _enableTime = 5f;

    private float _timer;
    
    private void Update()
    {
        if (_timer >= _enableTime)
        {
            _animator.enabled = true;
            _button.interactable = true;
            this.enabled = false;
        }

        _timer += Time.deltaTime;
    }
}
