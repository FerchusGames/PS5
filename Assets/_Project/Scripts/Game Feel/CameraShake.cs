using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField, Range(0, 1)] private float _intensityMultiplier;
    [SerializeField] private AnimationCurve _animationCurve;
    
    public IEnumerator Shaking()
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _animationCurve.Evaluate(elapsedTime / _duration);
            transform.localPosition = startPosition + (Random.insideUnitSphere * strength * _intensityMultiplier);
            yield return null;
        }

        transform.localPosition = startPosition;
    }
}
