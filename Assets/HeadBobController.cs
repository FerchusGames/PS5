using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;

    private float _originalFrequency;
    
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    [SerializeField] private MoveAlongPath _moveAlongPath;
    
    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private int _LastTime = 0;

    private float _currentTime = 0;
    
    private void Awake()
    {
        _startPos = _camera.localPosition;
        _originalFrequency = _frequency;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime * _frequency;
        _frequency = _originalFrequency * _moveAlongPath.GetSpeedPercentage();
        
        if (_enable == false) return;

        if (GameManager.Instance.GameState == GameState.gaming)
        {
            PlayMotion(FootStepMotion());
            RestetPositios();
            _camera.LookAt(FocusTarget());
            PlayFootstep();
        }
    }

    private void PlayFootstep()
    {
        int intTime = (int)(_currentTime);
        if (intTime > _LastTime)
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.FOOTSTEPS);
            _LastTime = intTime;
        }
    }

    private void RestetPositios()
    {
        if (_camera.localPosition == _startPos) return;

        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Mathf.PI * _currentTime * 2) * _amplitude;
        pos.x += Mathf.Cos(Mathf.PI * _currentTime ) * _amplitude * 2;
       
        return pos;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y,
            transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
    
    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion; 
    }
}
