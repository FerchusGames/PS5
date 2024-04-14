using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TrayController : MonoBehaviour
{
   private Vector3 _previousRotation;
   private Vector3 _currentRotation;

   [SerializeField] private Transform _playerTransform;
   
   [SerializeField, Range(1, 10)] private float _turnTiltRate;
   
   private void Update()
   {
      UpdateRotations();
      TurnTilt();
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
}
