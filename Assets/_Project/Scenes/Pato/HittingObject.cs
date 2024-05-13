using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingObject : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _volumeScale = 0.3f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag=="Food")
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.HIT, _volumeScale);
        }
    }
}
