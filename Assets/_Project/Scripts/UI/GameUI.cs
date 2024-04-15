using System;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        GameManager.Instance.OnScoreChange += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChange -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}

