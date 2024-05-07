using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrayController : MonoBehaviour
{
    private Vector3 _previousRotation;
    private Vector3 _currentRotation;
    
    [SerializeField] private Transform _playerTransform;
    [SerializeField, Range(1, 10)] private float _turnTiltRate;
    [SerializeField] private float tiltSpeed  = 0.1f; 
    
    private List<Rigidbody> _objectsInTray = new List<Rigidbody>();

    private void OnEnable()
    {
        GameManager.Instance.OnGameReset += Reset;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameReset -= Reset;
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.gaming)
            return;

        UpdateRotations();
        TurnTilt();
        if (_objectsInTray.Count > 0)
            WeightyTilt();
    }

    private void UpdateRotations()
    {
        Vector3 newRotation = transform.localRotation.eulerAngles;
        newRotation.y = 0;
        transform.localRotation = Quaternion.Euler(newRotation);
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

    private Vector3 CalculateCenterOfMass()
    {
        Vector3 centerMass = Vector3.zero;
        float totalMass = 0f;

        // Calculate the weighted sum of positions
        for (int i = 0; i < _objectsInTray.Count; i++)
        {
            centerMass += _objectsInTray[i].position * _objectsInTray[i].mass;
            totalMass += _objectsInTray[i].mass;
        }

        // Divide by the total mass to get the center of mass
        if (totalMass > 0f)
            centerMass /= totalMass;

        return centerMass;
    }

    private void OnCollisionEnter(Collision other)
    {
        // TODO: Cambiar la forma en la que se añaden y quitan los objetos porque está generando stutters
        // Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        // if (rb != null)
        // {
        //     _objectsInTray.Add(rb);
        // }
    }

    private void Reset()
    {
        // Transform
        _playerTransform.position = Vector3.zero;
        _playerTransform.rotation = Quaternion.identity;
        transform.localRotation = Quaternion.identity;

        _currentRotation = Vector3.zero;
        
        _objectsInTray.Clear();
    }
}
