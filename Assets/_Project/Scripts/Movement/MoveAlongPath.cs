    using System;
using UnityEngine;
using UnityEngine.Splines;
using Range = UnityEngine.SocialPlatforms.Range;

public class MoveAlongPath : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private float _minSpeed = 0.2f;
    [SerializeField] private GameObject _tray;
    [SerializeField] private Animator _countdownAnimation;
    [SerializeField] private Animation _finsihAnimation;
    [SerializeField, Range(0, 1)] private float _accelerationDistancePercentage = 0.05f;
    [SerializeField, Range(0, 1)] private float _decelerationDistancePercentage = 0.95f;
    [SerializeField] private float _stallTime = 3;
    private float _stallTimer = 0;

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
        _finsihAnimation = Camera.main.GetComponent<Animation>();
        Reset();
    }

    private void Reset()
    {
        DistancePercentage = 0;
        _speed = 0;
        _stallTimer = 0;

        _finsihAnimation.Stop();
        _countdownAnimation.SetTrigger("Reset");
        _countdownAnimation.speed = 1 / _stallTime;

        if (PlayerPrefs.GetInt("tutorial") != 0)
        {
            _countdownAnimation.SetTrigger("StartCountdown");
        }
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.gaming)
            return;

        _stallTimer += Time.deltaTime;
        
        if (_stallTimer >= _stallTime)
        {
            CalculateSpeed();
        }

        else
        {
            _tray.transform.localRotation = Quaternion.identity;
        }

        DistancePercentage += _speed * Time.deltaTime / _splineLength;

        SetCurrentPosition();

        if (DistancePercentage > 1f)
        {
            Reset();
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
        _countdownAnimation.speed = 1 / _stallTime;
    }
    
    public float GetSpeedPercentage()
    {
        float speedDifference = GameManager.Instance.GameValues.MaxSpeed - _minSpeed;

        return (_speed - _minSpeed) / speedDifference;
    }
}