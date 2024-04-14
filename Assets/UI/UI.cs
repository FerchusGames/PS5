
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void LoadScene(string sceneToLoad)
    {
        if (GameManager.Instance.gameStates == GameStates.pause)
        {
            GameManager.Instance.gameStates = GameStates.gaming;
        }
        
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    public void Pause()
    {
        GameManager.Instance.gameStates = GameStates.pause;
    }
    public void UnPause()
    {
        GameManager.Instance.gameStates = GameStates.gaming;
    }
    
}
