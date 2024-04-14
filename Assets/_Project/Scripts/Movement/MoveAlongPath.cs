using System;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongPath : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private float _speed;

    private SplineController _splineController;

    public event Action onSplineEndAction;
    
    private float _distancePercentage;
    private float _splineLength;

    private void Awake()
    {
        _splineController = GetComponent<SplineController>();
    }

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
            _spline = _splineController.NextSpline;
            onSplineEndAction?.Invoke();
        }

        Vector3 nextPosition = _spline.EvaluatePosition(_distancePercentage + 0.05f);
        Vector3 direction = nextPosition - currentPosition;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    public void SetSpline(SplineContainer splineContainer)
    {
        _spline = splineContainer;
    }
}