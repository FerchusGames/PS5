using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSound : MonoBehaviour
{
    
    public void PlayTapSound()
    {
        AudioManager.GetInstance().SetAudio(SOUND_TYPE.TAP);
    }
    
}
