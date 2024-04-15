using System;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }

    [SerializeField] private TMP_Text _highScoreText;

    [field:SerializeField] public int CurrentScore { get; private set; }
    
    private int _fallenFoodCount = 0;
    [SerializeField] private int _fallenFoodLimit = 5;

    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _mainGameUI;
    [SerializeField] private GameObject _controlsUI;
    
    public event Action<int> OnScoreChange;
    public event Action<GameState> OnGameStateChange;
    public event Action<int> OnHighScoreChange;

    public event Action OnGameReset;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetGameState(GameState.menu);

        _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void Reset()
    {
        _mainGameUI.SetActive(true);
        _controlsUI.SetActive(true);
        
        CurrentScore = 0;
        _fallenFoodCount = 0;
        SetGameState(GameState.gaming);
        OnGameReset?.Invoke();
    }

    public void AddScore(int scoreToAdd)
    {
        CurrentScore += scoreToAdd;
        
        CheckHighScore();
        
        OnScoreChange?.Invoke(CurrentScore);
    }

    public void AddFallenFood()
    {
        _fallenFoodCount++;
        CheckLose();
    }

    private void CheckLose()
    {
        if (_fallenFoodCount > _fallenFoodLimit)
        {
            Lose();
        }
    }
    
    public void SetGameState(GameState gameState)
    {
        GameState = gameState;
        OnGameStateChange?.Invoke(gameState);
    }
    
    public void Lose()
    {
        GameState = GameState.lose;
        _gameOverUI.SetActive(true);
        _mainGameUI.SetActive(false);
        _controlsUI.SetActive(false);
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", CurrentScore);
        PlayerPrefs.Save();
        OnHighScoreChange?.Invoke(CurrentScore);
    }
    
    private void CheckHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");

        if (CurrentScore > highScore)
        {
            SaveHighScore();
        }
    }
}

public enum GameState
{
    lose,
    pause,
    gaming,
    menu
}
