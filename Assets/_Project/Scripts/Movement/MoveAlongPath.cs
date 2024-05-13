    using System;
using UnityEngine;
using UnityEngine.Splines;
using Range = UnityEngine.SocialPlatforms.Range;

public class MoveAlongPath : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private float _maxSpeed = 15;
    [SerializeField] private float _minSpeed = 5;
    [SerializeField, Range(0, 1)] private float _accelerationDistancePercentage = 0.05f;
    [SerializeField, Range(0, 1)] private float _decelerationDistancePercentage = 0.95f;

    [SerializeField] private float _speed;
    
    private SplineController _splineController;

    public event Action OnSplineEndAction;
    
    public float DistancePercentage { get; private set; }
    private float _splineLength;

    private void Awake()
    {
        _splineController = GetComponent<SplineController>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameReset += Reset;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameReset -= Reset;
    }

    
    private void Start()
    {
        _splineLength = _spline.CalculateLength();
    }

    private void Reset()
    {
        DistancePercentage = 0;
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.gaming)
            return;
        
        CalculateSpeed();
        
        DistancePercentage += _speed * Time.deltaTime / _splineLength;

        SetCurrentPosition();

        if (DistancePercentage > 1f)
        {
            DistancePercentage = 0f;
            _spline = _splineController.NextSpline;
            SetCurrentPosition();
            OnSplineEndAction?.Invoke();
        }

        Vector3 nextPosition = _spline.EvaluatePosition(DistancePercentage + 0.05f);
        Vector3 direction = nextPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    private void CalculateSpeed()
    {
        float speedDifference = _maxSpeed - _minSpeed;
        
        if (DistancePercentage < _accelerationDistancePercentage)
        {
            float accelerationSpeedPercentage = (1 / _accelerationDistancePercentage) * DistancePercentage;
            _speed = _minSpeed + (Mathf.Sqrt(accelerationSpeedPercentage) * speedDifference);
        }
        else if (DistancePercentage > _decelerationDistancePercentage)
        {
            float decelerationSpeedPercentage = (1 / (1 - _decelerationDistancePercentage)) * (DistancePercentage - _decelerationDistancePercentage);
            _speed = _maxSpeed - (Mathf.Sqrt(decelerationSpeedPercentage) * speedDifference);
        }
        else
        {
            _speed = _maxSpeed;
        }
    }

    private void SetCurrentPosition()
    {
        Vector3 currentPosition = _spline.EvaluatePosition(DistancePercentage);
        transform.position = currentPosition;
    }

    public void SetSpline(SplineContainer splineContainer)
    {
        _spline = splineContainer;
    }

    public float GetSpeedPercentage()
    {
        float speedDifference = _maxSpeed - _minSpeed;

        return (_speed - _minSpeed) / speedDifference;
    }
}