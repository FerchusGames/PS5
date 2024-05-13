
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void LoadScene(string sceneToLoad)
    {
        if (GameManager.Instance.GameState == GameState.pause)
        {
            GameManager.Instance.SetGameState(GameState.gaming);
        }
        
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    public void Pause()
    {
        GameManager.Instance.SetGameState(GameState.pause);
    }
    public void UnPause()
    {
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            GameManager.Instance.OnUnPauseTuto();            
        }
        else
        {
            GameManager.Instance.SetGameState(GameState.gaming);
        }
    }
    
}
