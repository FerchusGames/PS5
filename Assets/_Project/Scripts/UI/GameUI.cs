using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;

    [SerializeField] private MoveAlongPath _moveAlongPath;
    [SerializeField] private Slider _slider;
    
    private void OnEnable()
    {
        GameManager.Instance.OnScoreChange += UpdateScore;
        GameManager.Instance.OnScoreChange += UpdateHighScore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChange -= UpdateScore;
        GameManager.Instance.OnScoreChange -= UpdateHighScore;
    }

    private void Start()
    {
        _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void UpdateHighScore(int highScore)
    {
        _highScoreText.text = highScore.ToString();
    }

    private void Update()
    {
        _slider.value = 1f - _moveAlongPath.DistancePercentage;
    }

    public void SetGameStateToGaming()
    {
        GameManager.Instance.SetGameState(GameState.gaming);
    }
}

