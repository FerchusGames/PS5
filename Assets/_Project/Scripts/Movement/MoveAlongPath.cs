    using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using Range = UnityEngine.SocialPlatforms.Range;

public class MoveAlongPath : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private float _minSpeed = 0.2f;
    [SerializeField] private GameObject _tray;
    [SerializeField] private Animator _countdownAnimation;
    [SerializeField] private Animator _finsihAnimation;
    [SerializeField, Range(0, 1)] private float _accelerationDistancePercentage = 0.05f;
    [SerializeField, Range(0, 1)] private float _decelerationDistancePercentage = 0.95f;
    [FormerlySerializedAs("_stallTime")] public float StallTime = 3;
    [HideInInspector] public float StallTimer = 0;

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
        _finsihAnimation = Camera.main.GetComponent<Animator>();
        Reset();
    }

    private void Reset()
    {
        DistancePercentage = 0;
        _speed = 0;
        StallTimer = 0;

        _finsihAnimation.enabled = false;
        //_countdownAnimation.SetTrigger("Reset");
        _countdownAnimation.speed = 1 / StallTime;

        
        _countdownAnimation.SetTrigger("StartCountdown");
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.gaming)
            return;

        StallTimer += Time.deltaTime;
        
        if (StallTimer >= StallTime)
        {
            CalculateSpeed();
        }

        DistancePercentage += _speed * Time.deltaTime / _splineLength;

        SetCurrentPosition();

        if (DistancePercentage < 1f)
        {
            Vector3 nextPosition = _spline.EvaluatePosition(DistancePercentage + 0.05f);
            Vector3 direction = nextPosition - transform.position;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);
        }
    }

    public void StartNextRun()
    {
        Reset();
        _spline = _splineController.NextSpline;
        SetCurrentPosition();
        OnSplineEndAction?.Invoke();
    }
    
    private void CalculateSpeed()
    {
        float speedDifference = GameManager.Instance.GameValues.MaxSpeed - _minSpeed;
        
        if (DistancePercentage < _accelerationDistancePercentage)
        {
            float accelerationSpeedPercentage = (1 / _accelerationDistancePercentage) * DistancePercentage;
            _speed = _minSpeed + (Mathf.Sqrt(accelerationSpeedPercentage) * speedDifference);
        }
        else if (DistancePercentage > _decelerationDistancePercentage)
        {
            float decelerationSpeedPercentage = (1 / (1 - _decelerationDistancePercentage)) * (DistancePercentage - _decelerationDistancePercentage);
            _speed = GameManager.Instance.GameValues.MaxSpeed - (Mathf.Sqrt(decelerationSpeedPercentage) * speedDifference);
        }
        else
        {
            _speed = GameManager.Instance.GameValues.MaxSpeed;
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

    public void PauseCountdown()
    {
        _countdownAnimation.speed = 0;
    }
    
    public void ResumeCountdown()
    {
        _countdownAnimation.speed = 1 / StallTime;
    }
    
    public float GetSpeedPercentage()
    {
        float speedDifference = GameManager.Instance.GameValues.MaxSpeed - _minSpeed;

        return (_speed - _minSpeed) / speedDifference;
    }
}