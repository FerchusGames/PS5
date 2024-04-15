using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    private TMP_Text _text;
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnHighScoreChange += UpdateHighScore;
        UpdateHighScore(0);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnHighScoreChange -= UpdateHighScore;
    }

    private void UpdateHighScore(int highScore)
    {
        _text.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
