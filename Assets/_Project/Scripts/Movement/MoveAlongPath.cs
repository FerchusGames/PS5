using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace PS5.Movement
{
    public class MoveAlongPath : MonoBehaviour
    {
        [SerializeField] private SplineContainer _spline;
        [SerializeField] private float _speed;
        
        private float _distancePercentage;
        private float _splineLength;

        private void Start()
        {
            _splineLength = _spline.CalculateLength();
        }

        private void Update()
        {
            _distancePercentage += _speed * Time.deltaTime / _splineLength;

            Vector3 currentPosition = _spline.EvaluatePosition(_distancePercentage);
            transform.position = currentPosition;

            if (_distancePercentage > 1f)
            {
                _distancePercentage = 0f;
            }

            Vector3 nextPosition = _spline.EvaluatePosition(_distancePercentage + 0.05f);
            Vector3 direction = nextPosition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);
        }
    }
}
