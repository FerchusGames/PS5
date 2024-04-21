using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class SplineController : MonoBehaviour
{
    [field:SerializeField] public SplineContainer CurrentSpline { get; private set; }
    [field:SerializeField] public SplineContainer NextSpline { get; private set; }

    [SerializeField] private List<GameObject> _splinePrefabs;
    
    private MoveAlongPath _moveAlongPath;
    
    private void Awake()
    {
        _moveAlongPath = GetComponent<MoveAlongPath>();

        _moveAlongPath.SetSpline(CurrentSpline);
    }
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameReset += Reset;
        _moveAlongPath.onSplineEndAction += CreateNextSpline;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.OnGameReset -= Reset;
        _moveAlongPath.onSplineEndAction -= CreateNextSpline;
    }

    private void Start()
    {
        CreateNextSpline();
    }

    private void Reset()
    {
        CreateNextSpline();
    }

    private void CreateNextSpline()
    {
        Destroy(CurrentSpline.gameObject);
        int nextSplineIndex = Random.Range(0, _splinePrefabs.Count);
        GameObject nextSplineObject = Instantiate(_splinePrefabs[nextSplineIndex]);
        NextSpline = nextSplineObject.GetComponent<SplineContainer>();
        CurrentSpline = NextSpline;
        _moveAlongPath.SetSpline(CurrentSpline);
    }
}
