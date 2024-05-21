using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireworksAudio : MonoBehaviour
{
    [SerializeField] private float _minLaunchTime = 0.2f;
    [SerializeField] private float _maxLaunchTime = 0.4f;
    [SerializeField] private float _maxExplodeTime = 0.5f;
    [SerializeField] private float _minExplodeTime = 0.9f;

    private float _launchTimer = 0;
    private float _explodeTimer = 0;

    private float _launchTime;
    private float _explodeTime;
    

    private void Update()
    {
        if (_launchTimer == 0)
        {
            _launchTime = Random.Range(_minLaunchTime, _maxLaunchTime);
        }
        
        if (_explodeTimer == 0)
        {
            _explodeTime = Random.Range(_minExplodeTime, _maxExplodeTime);
        }

        _launchTimer += Time.deltaTime; 
        _explodeTimer += Time.deltaTime; 
        
        if (_launchTimer >= _launchTime)
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.FIREWORK_LAUNCH);
            _launchTimer = 0;
        }
        
        if (_explodeTimer >= _explodeTime)
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.FIREWORK_EXPLODE);
            _explodeTimer = 0;
        }
    }
}
