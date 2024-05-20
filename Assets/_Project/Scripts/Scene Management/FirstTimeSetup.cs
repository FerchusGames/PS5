using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FirstTimeSetup : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    
    private void Start()
    {
        float sfxLevel = PlayerPrefs.GetFloat("sfxLevel", 1f);
        float musicLevel = PlayerPrefs.GetFloat("musicLevel", 1f);
        
        _audioMixer.SetFloat("sfx_vol", PercentageToVolume(sfxLevel));
        _audioMixer.SetFloat("music_vol", PercentageToVolume(musicLevel));
    }

    private float PercentageToVolume(float percentage)
    {
        return (1 - Mathf.Sqrt(percentage)) * -80;
    }
}
