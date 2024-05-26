using System;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field: SerializeField] public GameValues GameValues;
    [field: SerializeField] public GameValues DefaultGameValues;
    [field: SerializeField] public GameValues EndGameValues { get; private set; }

    [field:SerializeField] public GameState GameState { get; private set; }

    [field:SerializeField] public int CurrentScore { get; private set; }
    
    private int _fallenFoodCount = 0;
    [SerializeField] private int _fallenFoodLimit = 5;

    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _mainGameUI;
    [SerializeField] private GameObject _controlsUI;

    [SerializeField] private Spawn _spawn;
    [SerializeField] private GameObject _PopUps;
    
    public bool IsTutorial { get; private set; } = false;
    
    public event Action<int> OnScoreChange;
    public event Action<GameState> OnGameStateChange;
    public event Action<int> OnHighScoreChange;

    public event Action OnGameReset;
    public event Action OnGameStart;

    [SerializeField] private MoveAlongPath _moveAlongPath;
    
    private float _initialTurningTiltRate;
    private float _initialWeightTiltRate;
    private float _turningTiltRateDifference;
    private float _weightTiltRateDifference;
    
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
        _moveAlongPath.OnSplineEndAction += MoveAlongPathOnOnSplineEndAction;
    }

    private void MoveAlongPathOnOnSplineEndAction()
    {
        GameValues.TurningTiltRate = _initialTurningTiltRate + ((CurrentScore / 999f) * _turningTiltRateDifference);
        GameValues.WeightTiltRate = _initialWeightTiltRate + ((CurrentScore / 999f) * _weightTiltRateDifference);
        
        Debug.Log($"Turning Tilt Rate: {GameValues.TurningTiltRate}, Weight Tilt Rate: {GameValues.WeightTiltRate}");
    }

    private void OnDisable()
    {
        _spawn.OnSpawnObjects -= ResetFallenFood;
        _moveAlongPath.OnSplineEndAction -= MoveAlongPathOnOnSplineEndAction;
    }

    private void Start()
    {
        SetGameState(GameState.menu);

        ResetGameValues();

        _initialTurningTiltRate = DefaultGameValues.TurningTiltRate;
        _initialWeightTiltRate = DefaultGameValues.WeightTiltRate;
        
        _turningTiltRateDifference = EndGameValues.TurningTiltRate - DefaultGameValues.TurningTiltRate;
        _weightTiltRateDifference = EndGameValues.WeightTiltRate - DefaultGameValues.WeightTiltRate;
    }

    private void ResetGameValues()
    {
        GameValues.TurningTiltRate = DefaultGameValues.TurningTiltRate;
        GameValues.WeightTiltRate = DefaultGameValues.WeightTiltRate;
    }

    public void EndTuto()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        PlayerPrefs.Save();
        GoToMainGame();
    }
    
    public void PlayTuto()
    {
        SceneManager.LoadScene("Tutorial");
        //Reset();
        //ResetPopUps();
        //OnGameStart?.Invoke();
    }

    public void GoToMainGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void StartTuto()
    {
        ResetPopUps();
        SetGameState(GameState.gaming);
        IsTutorial = true;
    }

    public void ResetPopUps()
    {
        _mainGameUI.SetActive(true);
        _controlsUI.SetActive(true);
        if (_PopUps)
            _PopUps.SetActive(true);

        AddScore(-CurrentScore);
        
        _fallenFoodCount = 0;
        OnGameReset?.Invoke();
    }

    public void OnLoseTuto()
    {
        if (_PopUps)
        _PopUps.SetActive(false);
        
        AddScore(-CurrentScore);
        
        _fallenFoodCount = 0;
    }

    public void OnUnPauseTuto()
    {
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            if (_PopUps)
            _PopUps.SetActive(true);
        }
        SetGameState(GameState.gaming);
    }
    
    public void StartGame()
    {
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            PlayTuto();
        }
        else
        {
            Reset();
            SetGameState(GameState.gaming);
            OnGameStart?.Invoke();
        }
    }

    public void Reset()
    {
        ResetGameValues();
        Debug.Log($"Turning Tilt Rate: {GameValues.TurningTiltRate}, Weight Tilt Rate: {GameValues.WeightTiltRate}");
        
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

        if (CurrentScore > 999)
        {
            CurrentScore = 999;
        }
        
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
        if (_PopUps)
        _PopUps.SetActive(false);
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
}
