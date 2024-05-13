using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRigidbody : MonoBehaviour
{
    private Rigidbody _rigidbody; 
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        TrayController.Instance.AddObjectRigidbody(_rigidbody);
    }

    private void OnDestroy()
    {
        TrayController.Instance.RemoveObjectRigidbody(_rigidbody);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += CheckFreeze;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= CheckFreeze;
    }

    private void CheckFreeze(GameState gameState)
    {
        if (gameState == GameState.gaming)
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
        }

        else
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
