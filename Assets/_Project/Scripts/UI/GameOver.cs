using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _controls;
    [SerializeField] private GameObject _mainGameUI;

    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _finalScoreText;

    private void OnEnable()
    {
        _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        _finalScoreText.text = GameManager.Instance.CurrentScore.ToString();
    }

    public void GoToMenu()
    {
        GameManager.Instance.Reset();    
        GameManager.Instance.SetGameState(GameState.menu);
        _menu.SetActive(true);
        _controls.SetActive(false);
        _mainGameUI.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        GameManager.Instance.Reset();        
        GameManager.Instance.SetGameState(GameState.gaming);
        gameObject.SetActive(false);
    }
}
