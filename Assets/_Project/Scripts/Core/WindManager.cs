using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WindManager : MonoBehaviour
{
    public static WindManager Instance { get; private set; }
    public WindState WindState { get; private set; }
    
    public Vector3 WindDirection { get; private set; }
    public float WindMagnitude { get; private set; }

    [SerializeField] private float _minWindMagnitude;
    [SerializeField] private float _maxWindMagnitude;
    
    [Header("Time")]
    [SerializeField] private float _minWindWaitTime;
    [SerializeField] private float _maxWindWaitTime;
    [SerializeField] private float _minWindActiveTime;
    [SerializeField] private float _maxWindActiveTime;

    private float _windWaitTime;
    private float _windActiveTime;

    private float _timer;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        _windWaitTime = Random.Range(_minWindWaitTime, _maxWindWaitTime);
        _windActiveTime = Random.Range(_minWindActiveTime, _maxWindActiveTime);

        SetWindProperties();

        WindState = WindState.wait;
        _timer = _windWaitTime;
    }

    private void Update()
    {
        CheckTimer();
    }

    private void CheckTimer()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            ChangeWindState();
        }
    }

    private void ChangeWindState()
    {
        switch (WindState)
        {
            case WindState.active:
                _windWaitTime = Random.Range(_minWindWaitTime, _maxWindWaitTime);
                _timer = _windWaitTime;
                WindState = WindState.wait;
                SetWindProperties();
                break;
                
            case WindState.wait:
                _windActiveTime = Random.Range(_minWindActiveTime, _maxWindActiveTime);
                _timer = _windActiveTime;
                WindState = WindState.active;
                break;
        }
    }

    private void SetWindProperties()
    {
        WindDirection = GetNextWindDirection();
        WindMagnitude = Random.Range(_minWindMagnitude, _maxWindMagnitude);
    }

    private Vector3 GetNextWindDirection()
    {
        // Generate a random angle in radians
        float angle = Random.Range(0f, 2f * Mathf.PI);

        // Use Mathf.Cos and Mathf.Sin to get a vector on the XZ plane
        float x = Mathf.Cos(angle);
        float z = Mathf.Sin(angle);
        
        return new Vector3(x, 0f, z);
    }
}

public enum WindState
{
    wait,
    active
}
