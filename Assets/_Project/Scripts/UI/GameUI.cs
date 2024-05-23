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
    [SerializeField] private Animation _animation;
    
    private void OnEnable()
    {
        GameManager.Instance.OnScoreChange += UpdateScore;
        GameManager.Instance.OnHighScoreChange += UpdateHighScore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChange -= UpdateScore;
        GameManager.Instance.OnHighScoreChange -= UpdateHighScore;
    }

    private void Start()
    {
        if (!_highScoreText) return;
        _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void UpdateScore(int score)
    {
        if(!_scoreText) return;
        _scoreText.text = score.ToString();
        _animation.Play();
    }

    private void UpdateHighScore(int highScore)
    {
        if (!_highScoreText) return; 
        _highScoreText.text = highScore.ToString();
    }

    private void Update()
    {
        _slider.value = _moveAlongPath.DistancePercentage;
    }

    public void SetGameStateToGaming()
    {
        GameManager.Instance.SetGameState(GameState.gaming);
    }
}

