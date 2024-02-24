using System;
using UnityEngine;
using UnityEngine.Splines;

namespace PS5.Movement
{
    public class SplineController : MonoBehaviour
    {
        [field:SerializeField] public SplineContainer CurrentSpline { get; private set; }
        [field:SerializeField] public SplineContainer NextSpline { get; private set; }

        private MoveAlongPath _moveAlongPath;

        private void Awake()
        {
            _moveAlongPath = GetComponent<MoveAlongPath>();

            _moveAlongPath.SetSpline(CurrentSpline);
            
            _moveAlongPath.onSplineEndAction += CreateNextSpline;
        }

        private void CreateNextSpline()
        {
            CurrentSpline = NextSpline;

            NextSpline = null; //TODO Generate the next spline and assign it.
            
            Debug.Log("Creating Next Spline");
        }
    }
}
