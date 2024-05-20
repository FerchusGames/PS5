using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CountdownAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _countBeep;
    [SerializeField] private AudioClip _startBeep;

    [SerializeField] private AudioSource _audioSource;
    
    public void PlayCountBeep()
    {
        _audioSource.PlayOneShot(_countBeep, 0.3f);
    }

    public void PlayStartBeep()
    {
        _audioSource.PlayOneShot(_startBeep, 0.3f);
    }
}
