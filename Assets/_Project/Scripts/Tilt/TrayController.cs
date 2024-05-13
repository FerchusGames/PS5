using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class TrayController : MonoBehaviour
{
    public static TrayController Instance { get; private set; }
    
    private Vector3 _previousRotation;
    private Vector3 _currentRotation;
    
    [SerializeField] private Transform _playerTransform;
    [SerializeField, Range(1, 10)] private float _turnTiltRate;
    [SerializeField] private float tiltSpeed  = 0.1f;

    private List<Rigidbody> objectsInTray = new List<Rigidbody>();

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
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameReset += ResetTransform;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameReset -= ResetTransform;
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.gaming)
            return;

        UpdateRotations();
        TurnTilt();
        if (objectsInTray.Count > 0)
            WeightyTilt();
    }

    private void UpdateRotations()
    {
        _previousRotation = _currentRotation;
        _currentRotation = _playerTransform.rotation.eulerAngles;
    }

    private float GetTurningRate()
    {
        float deltaY = _currentRotation.y - _previousRotation.y;

        // Correct for angle wrapping
        if (deltaY > 180) deltaY -= 360;
        else if (deltaY < -180) deltaY += 360;

        return deltaY / Time.deltaTime; // Turning rate in degrees per second
    }
    
    private void TurnTilt()
    {
        transform.Rotate(new Vector3(0, 0, 1) * (GetTurningRate() * _turnTiltRate * 0.0001f), Space.Self);
    }

    private void WeightyTilt()
    {
        Vector3 centerOfMass = CalculateCenterOfMass();
        
        // Calculate tilt angle
        Vector3 trayCenter = transform.position;
        Vector3 tiltDirection = centerOfMass - trayCenter;
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, tiltDirection);

        // Apply tilt smoothly
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }

    public void RemoveFromList(Rigidbody rb)
    {
        objectsInTray.Remove(rb);
    }

    private Vector3 CalculateCenterOfMass()
    {
        Vector3 centerMass = Vector3.zero;
        float totalMass = 0f;

        // Calculate the weighted sum of positions
        for (int i = 0; i < objectsInTray.Count; i++)
        {
            centerMass += objectsInTray[i].position * objectsInTray[i].mass;
            totalMass += objectsInTray[i].mass;
        }

        // Divide by the total mass to get the center of mass
        if (totalMass > 0f)
            centerMass /= totalMass;

        return centerMass;
    }

    public void AddObjectRigidbody(Rigidbody rigidbody)
    {
        _objectsInTray.Add(rigidbody);
    }
    
    public void RemoveObjectRigidbody(Rigidbody rigidbody)
    {
        _objectsInTray.Remove(rigidbody);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            objectsInTray.Add(rb);
        }
    }

    private void ResetTransform()
    {
        _playerTransform.position = Vector3.zero;
        _playerTransform.rotation = Quaternion.identity;
        transform.rotation = Quaternion.identity;
        objectsInTray.Clear();
    }
}
