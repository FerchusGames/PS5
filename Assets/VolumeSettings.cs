using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    AudioMixer _audioMixer;
    float _volume = 1;
    float _DbVolume = 0;
    
    private void Awake()
    {
        _volume = 1;
    }
    void CambiarVolumenMixer()
    {
        _DbVolume = (_volume * 80) - 80;
        //_DbVolume = (1 - Mathf.Sqrt(_volume) * -80);
        _audioMixer.SetFloat("sfx_vol", _DbVolume);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _volume += 0.1f;
            if(_volume>=1)
            {
                _volume = 1;
            }
            _DbVolume = (_volume * 80) - 80;
            //_DbVolume = (1 - Mathf.Sqrt(_volume) * -80);
            _audioMixer.SetFloat("sfx_vol",_DbVolume);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _volume -= 0.1f;
            if (_volume <= 1)
            {
                _volume = 0;
            }
            _DbVolume = (_volume * 80) - 80;
            //_DbVolume = (1 - Mathf.Sqrt(_volume) * -80);
            _audioMixer.SetFloat("sfx_vol", _DbVolume);
        }
    }
    }
}
