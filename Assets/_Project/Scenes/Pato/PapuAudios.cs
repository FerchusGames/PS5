using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapuAudios : MonoBehaviour
{
    private static PapuAudios _instance;
    public static PapuAudios GetInstance()
    {
        return _instance;
    }
    private void Awake()
    {
         _instance = this;
        SelfAudioSource = GetComponent<AudioSource>();    
    }

    AudioSource SelfAudioSource;
    AudioLibrary library;
    [SerializeField] GameObject audioSourcePrefab;
    private List<AudioSource> audioSources = new List<AudioSource>();
    public void SetAudio(SOUND_TYPE _requestSound)
    {
        SelfAudioSource.PlayOneShot(library.GetAudio(_requestSound));
    }
    public void SetAudio(SOUND_TYPE _requestSound, Vector3 position)
    {
        AudioSource newAudio = GetAudioSource();
        newAudio.transform.position = position;
        newAudio.clip = library.GetAudio(_requestSound);
        newAudio.Play();

    }

    AudioSource GetAudioSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                return audioSources[i];
            }
        }

        AudioSource newAudio = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
        audioSources.Add(newAudio);
        return newAudio;
    }

}




