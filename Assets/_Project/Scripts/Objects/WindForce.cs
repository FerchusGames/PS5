using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (WindManager.Instance.WindState == WindState.active)
        {
            _rigidbody.AddForce(WindManager.Instance.GetWind());
        }
    }
}
