using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingObject : MonoBehaviour
{
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag=="Food")
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.HIT);
        }
    }
}
