using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    float _DbVolume = 0;
    [SerializeField] Slider _slider;
    [SerializeField]  MixerChannel _mixerChannel;
    
    private void OnEnable()
    {
        if (_mixerChannel == MixerChannel.SFX)
        {
            _slider.value = PlayerPrefs.GetFloat("sfxLevel", 1f);
        }

        if (_mixerChannel == MixerChannel.MUSIC)
        {
           _slider.value = PlayerPrefs.GetFloat("musicLevel", 1f);;
        }
    }

    public enum MixerChannel
    {
        SFX,
        MUSIC
    }
    
    public void CambiarVolumenMixer()
    {
        //_DbVolume = (_slider.value * 80) - 80;
        _DbVolume = ((1-Mathf.Sqrt(_slider.value)) * -80);
        if (_mixerChannel == MixerChannel.SFX)
        {
            _audioMixer.SetFloat("sfx_vol", _DbVolume);
            PlayerPrefs.SetFloat("sfxLevel", _slider.value);
        }

        if (_mixerChannel == MixerChannel.MUSIC)
        {
            _audioMixer.SetFloat("music_vol", _DbVolume);
            PlayerPrefs.SetFloat("musicLevel", _slider.value);
        }
    }
}
