using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName="AudioLibrary" ,menuName = "Scriptables/AudioLibrary", order = 0)]

public class AudioLibrary : ScriptableObject
{
    [SerializeField] AudioData[] audios;


    public AudioClip GetAudio(SOUND_TYPE _soundRequest)
    {
        for (int i = 0; i < audios.Length; i++)
        {
            if (audios[i].soundType == _soundRequest)
            {
                int randomNumber = UnityEngine.Random.Range(0, audios[i].clip.Length);
                return audios[i].clip[randomNumber];
            }
        }
        return null;
    }

}
[Serializable]
public class AudioData
{
    public SOUND_TYPE soundType;
    public AudioClip[] clip;
}
public enum SOUND_TYPE
{
    FOOTSTEPS,
    HIT,
    DELIVER,
    SLIDE,
    WIND,
    MUSIC
}
