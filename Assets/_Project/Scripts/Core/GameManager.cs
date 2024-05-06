using System;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [field:SerializeField] public GameState GameState { get; private set; }

    [field:SerializeField] public int CurrentScore { get; private set; }
    
    private int _fallenFoodCount = 0;
    [SerializeField] private int _fallenFoodLimit = 5;

    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _mainGameUI;
    [SerializeField] private GameObject _controlsUI;

    [SerializeField] private Spawn _spawn;
    [SerializeField] private GameObject _PopUps;
    
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

    private void OnEnable()
    {
        _spawn.OnSpawnObjects += ResetFallenFood;
    }
    
    private void OnDisable()
    {
        _spawn.OnSpawnObjects -= ResetFallenFood;
    }

    private void Start()
    {
        Reset();
        SetGameState(GameState.menu);
    }

    public void StartTuto()
    {
        Reset();
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            SetGameState(GameState.tutorial);
        }
        else
        {
            StartGame();
        }
    }

    public void EndTuto()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        PlayerPrefs.Save();
        SetGameState(GameState.gaming);
    }
    
    public void PlayTuto()
    {
        Reset();
        PlayerPrefs.SetInt("tutorial", 0);
        Debug.Log(PlayerPrefs.GetInt("tutorial"));
        ResetPopUps();
        SetGameState(GameState.tutorial);
    }

    public void ResetPopUps()
    {
        _mainGameUI.SetActive(true);
        _controlsUI.SetActive(true);
        _PopUps.SetActive(true);

        AddScore(-CurrentScore);
        
        _fallenFoodCount = 0;
        OnGameReset?.Invoke();
    }
    
    public void StartGame()
    {
        Reset();
        SetGameState(GameState.gaming);
    }

    public void Reset()
    {
        _mainGameUI.SetActive(true);
        _controlsUI.SetActive(true);

        AddScore(-CurrentScore);
        
        _fallenFoodCount = 0;
        OnGameReset?.Invoke();
    }

    private void ResetFallenFood(int newFallenFoodLimit)
    {
        _fallenFoodCount = 0;
        _fallenFoodLimit = newFallenFoodLimit;
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
        if (_fallenFoodCount >= _fallenFoodLimit)
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
        SetGameState(GameState.lose);
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

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
        OnHighScoreChange?.Invoke(CurrentScore);
    }
}

public enum GameState
{
    lose,
    pause,
    gaming,
    menu,
    tutorial
}
