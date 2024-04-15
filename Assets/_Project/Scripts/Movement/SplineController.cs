using System;
using UnityEngine;
using UnityEngine.Splines;

public class SplineController : MonoBehaviour
{
    [field:SerializeField] public SplineContainer CurrentSpline { get; private set; }
    [field:SerializeField] public SplineContainer NextSpline { get; private set; }

    private MoveAlongPath _moveAlongPath;
    
    private void Awake()
    {
        _moveAlongPath = GetComponent<MoveAlongPath>();

        _moveAlongPath.SetSpline(CurrentSpline);
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
        _moveAlongPath.onSplineEndAction += CreateNextSpline;
    }

    private void Reset()
    {
        //CreateNextSpline();
    }

    private void CreateNextSpline()
    {
        CurrentSpline = NextSpline;

        NextSpline = null; //TODO Generate the next spline and assign it.
        
        Debug.Log("Creating Next Spline");
    }
}
