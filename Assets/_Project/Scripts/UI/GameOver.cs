using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _finalScoreText;

    private void OnEnable()
    {
        _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        _finalScoreText.text = GameManager.Instance.CurrentScore.ToString();
    }

    public void GoToMenu()
    {
        GameManager.Instance.SetGameState(GameState.menu);
        _menu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        GameManager.Instance.Reset();
        gameObject.SetActive(false);
    }
}
