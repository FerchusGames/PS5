using System;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongPath : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private float _speed;

    private SplineController _splineController;

    public event Action onSplineEndAction;
    
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
        
        DistancePercentage += _speed * Time.deltaTime / _splineLength;

        Vector3 currentPosition = _spline.EvaluatePosition(DistancePercentage);
        transform.position = currentPosition;

        if (DistancePercentage > 1f)
        {
            DistancePercentage = 0f;
            _spline = _splineController.NextSpline;
            onSplineEndAction?.Invoke();
        }

        Vector3 nextPosition = _spline.EvaluatePosition(DistancePercentage + 0.05f);
        Vector3 direction = nextPosition - currentPosition;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    public void SetSpline(SplineContainer splineContainer)
    {
        _spline = splineContainer;
    }
}